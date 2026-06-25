import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Instrument } from '../../core/models/instrument.model';
import {
  PracticeSession,
  PracticeSessionFilters,
  PracticeSessionUpsertRequest,
} from '../../core/models/practice-session.model';
import { InstrumentsService } from '../../core/services/instruments.service';
import { PracticeSessionsService } from '../../core/services/practice-sessions.service';

interface SessionFormModel {
  instrumentId: string;
  startTime: string;
  endTime: string;
  notes: string;
}

@Component({
  selector: 'app-sessions-page',
  imports: [CommonModule, FormsModule],
  templateUrl: './sessions-page.html',
  styleUrl: './sessions-page.css',
})
export class SessionsPage implements OnInit, OnDestroy {
  private readonly instrumentsService = inject(InstrumentsService);
  private readonly practiceSessionsService = inject(PracticeSessionsService);

  protected instruments: Instrument[] = [];
  protected sessions: PracticeSession[] = [];
  protected loading = true;
  protected error = '';
  protected editingSessionId: string | null = null;
  protected filters = {
    instrumentId: '',
    from: '',
    to: '',
  };
  protected formModel: SessionFormModel = this.createEmptySessionForm();
  protected timerInstrumentId = '';
  protected timerNotes = '';
  protected timerElapsed = '00:00:00';
  protected timerStartedAt: Date | null = null;

  private timerIntervalId: ReturnType<typeof setInterval> | null = null;

  ngOnInit(): void {
    this.loadInstruments();
    this.loadSessions();
  }

  ngOnDestroy(): void {
    this.clearTimer();
  }

  protected loadSessions(): void {
    this.loading = true;
    this.error = '';

    const filters: PracticeSessionFilters = {
      instrumentId: this.filters.instrumentId || undefined,
      from: this.filters.from ? this.toUtcRangeStart(this.filters.from) : undefined,
      to: this.filters.to ? this.toUtcRangeEnd(this.filters.to) : undefined,
    };

    this.practiceSessionsService.list(filters).subscribe({
      next: (sessions) => {
        this.sessions = sessions;
        this.loading = false;
      },
      error: () => {
        this.error = 'Unable to load practice sessions.';
        this.loading = false;
      },
    });
  }

  protected loadInstruments(): void {
    this.instrumentsService.list().subscribe({
      next: (instruments) => {
        this.instruments = instruments;
        if (!this.formModel.instrumentId && instruments[0]) {
          this.formModel.instrumentId = instruments[0].id;
        }
        if (!this.timerInstrumentId && instruments[0]) {
          this.timerInstrumentId = instruments[0].id;
        }
      },
      error: () => {
        this.error = 'Unable to load instruments required for sessions.';
      },
    });
  }

  protected save(): void {
    if (!this.formModel.instrumentId || !this.formModel.startTime || !this.formModel.endTime) {
      this.error = 'Instrument, start time, and end time are required.';
      return;
    }

    const payload = this.toSessionPayload(this.formModel);
    const request$ = this.editingSessionId
      ? this.practiceSessionsService.update(this.editingSessionId, payload)
      : this.practiceSessionsService.create(payload);

    request$.subscribe({
      next: () => {
        this.resetForm();
        this.loadSessions();
      },
      error: (response) => {
        this.error = response?.error?.message ?? 'Unable to save the session.';
      },
    });
  }

  protected edit(session: PracticeSession): void {
    this.editingSessionId = session.id;
    this.formModel = {
      instrumentId: session.instrumentId,
      startTime: this.toLocalDateTimeInput(session.startTime),
      endTime: this.toLocalDateTimeInput(session.endTime),
      notes: session.notes ?? '',
    };
  }

  protected cancelEdit(): void {
    this.resetForm();
  }

  protected remove(session: PracticeSession): void {
    if (!window.confirm(`Delete the session for ${session.instrumentName}?`)) {
      return;
    }

    this.practiceSessionsService.delete(session.id).subscribe({
      next: () => this.loadSessions(),
      error: () => {
        this.error = 'Unable to delete the session.';
      },
    });
  }

  protected startTimer(): void {
    if (!this.timerInstrumentId) {
      this.error = 'Select an instrument before starting the timer.';
      return;
    }

    this.error = '';
    this.timerStartedAt = new Date();
    this.updateTimerElapsed();
    this.clearTimer();
    this.timerIntervalId = setInterval(() => this.updateTimerElapsed(), 1000);
  }

  protected stopTimer(): void {
    if (!this.timerStartedAt || !this.timerInstrumentId) {
      return;
    }

    const payload: PracticeSessionUpsertRequest = {
      instrumentId: this.timerInstrumentId,
      startTime: this.timerStartedAt.toISOString(),
      endTime: new Date().toISOString(),
      notes: this.timerNotes.trim() || null,
    };

    this.practiceSessionsService.create(payload).subscribe({
      next: () => {
        this.timerNotes = '';
        this.clearTimer();
        this.timerStartedAt = null;
        this.timerElapsed = '00:00:00';
        this.loadSessions();
      },
      error: (response) => {
        this.error = response?.error?.message ?? 'Unable to save the timed session.';
      },
    });
  }

  protected cancelTimer(): void {
    this.timerNotes = '';
    this.timerStartedAt = null;
    this.timerElapsed = '00:00:00';
    this.clearTimer();
  }

  private updateTimerElapsed(): void {
    if (!this.timerStartedAt) {
      this.timerElapsed = '00:00:00';
      return;
    }

    const elapsedMilliseconds = Date.now() - this.timerStartedAt.getTime();
    const totalSeconds = Math.max(0, Math.floor(elapsedMilliseconds / 1000));
    const hours = Math.floor(totalSeconds / 3600).toString().padStart(2, '0');
    const minutes = Math.floor((totalSeconds % 3600) / 60).toString().padStart(2, '0');
    const seconds = (totalSeconds % 60).toString().padStart(2, '0');
    this.timerElapsed = `${hours}:${minutes}:${seconds}`;
  }

  private clearTimer(): void {
    if (this.timerIntervalId) {
      clearInterval(this.timerIntervalId);
      this.timerIntervalId = null;
    }
  }

  private resetForm(): void {
    this.editingSessionId = null;
    this.formModel = this.createEmptySessionForm();
    if (this.instruments[0]) {
      this.formModel.instrumentId = this.instruments[0].id;
    }
  }

  private createEmptySessionForm(): SessionFormModel {
    return {
      instrumentId: '',
      startTime: '',
      endTime: '',
      notes: '',
    };
  }

  private toSessionPayload(formModel: SessionFormModel): PracticeSessionUpsertRequest {
    return {
      instrumentId: formModel.instrumentId,
      startTime: new Date(formModel.startTime).toISOString(),
      endTime: new Date(formModel.endTime).toISOString(),
      notes: formModel.notes.trim() || null,
    };
  }

  private toLocalDateTimeInput(value: string): string {
    const date = new Date(value);
    const timezoneOffset = date.getTimezoneOffset() * 60000;
    return new Date(date.getTime() - timezoneOffset).toISOString().slice(0, 16);
  }

  private toUtcRangeStart(value: string): string {
    return new Date(`${value}T00:00:00`).toISOString();
  }

  private toUtcRangeEnd(value: string): string {
    return new Date(`${value}T23:59:59`).toISOString();
  }
}
