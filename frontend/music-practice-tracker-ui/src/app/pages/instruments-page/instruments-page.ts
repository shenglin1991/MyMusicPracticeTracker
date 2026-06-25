import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Instrument, InstrumentUpsertRequest } from '../../core/models/instrument.model';
import { InstrumentsService } from '../../core/services/instruments.service';

@Component({
  selector: 'app-instruments-page',
  imports: [CommonModule, FormsModule],
  templateUrl: './instruments-page.html',
  styleUrl: './instruments-page.css',
})
export class InstrumentsPage implements OnInit {
  private readonly instrumentsService = inject(InstrumentsService);

  protected instruments: Instrument[] = [];
  protected loading = true;
  protected error = '';
  protected saving = false;
  protected editingInstrumentId: string | null = null;
  protected formModel: InstrumentUpsertRequest = this.createEmptyForm();

  ngOnInit(): void {
    this.load();
  }

  protected load(): void {
    this.loading = true;
    this.error = '';

    this.instrumentsService.list().subscribe({
      next: (instruments) => {
        this.instruments = instruments;
        this.loading = false;
      },
      error: () => {
        this.error = 'Unable to load instruments.';
        this.loading = false;
      },
    });
  }

  protected save(): void {
    if (!this.formModel.name.trim()) {
      this.error = 'Instrument name is required.';
      return;
    }

    this.saving = true;
    this.error = '';
    const payload = {
      name: this.formModel.name.trim(),
      color: this.formModel.color,
    };

    const request$ = this.editingInstrumentId
      ? this.instrumentsService.update(this.editingInstrumentId, payload)
      : this.instrumentsService.create(payload);

    request$.subscribe({
      next: () => {
        this.resetForm();
        this.load();
        this.saving = false;
      },
      error: () => {
        this.error = 'Unable to save the instrument.';
        this.saving = false;
      },
    });
  }

  protected edit(instrument: Instrument): void {
    this.editingInstrumentId = instrument.id;
    this.formModel = {
      name: instrument.name,
      color: instrument.color,
    };
  }

  protected cancelEdit(): void {
    this.resetForm();
  }

  protected remove(instrument: Instrument): void {
    if (!window.confirm(`Delete ${instrument.name} and its practice sessions?`)) {
      return;
    }

    this.instrumentsService.delete(instrument.id).subscribe({
      next: () => this.load(),
      error: () => {
        this.error = 'Unable to delete the instrument.';
      },
    });
  }

  private resetForm(): void {
    this.formModel = this.createEmptyForm();
    this.editingInstrumentId = null;
  }

  private createEmptyForm(): InstrumentUpsertRequest {
    return {
      name: '',
      color: '#2563eb',
    };
  }
}
