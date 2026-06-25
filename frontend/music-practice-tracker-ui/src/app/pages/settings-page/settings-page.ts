import { CommonModule } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { GoalSettings } from '../../core/models/goal-settings.model';
import { GoalsService } from '../../core/services/goals.service';

@Component({
  selector: 'app-settings-page',
  imports: [CommonModule, FormsModule],
  templateUrl: './settings-page.html',
  styleUrl: './settings-page.css',
})
export class SettingsPage implements OnInit {
  private readonly goalsService = inject(GoalsService);

  protected goalSettings: GoalSettings = {
    dailyTargetMinutes: 30,
    weeklyTargetMinutes: 180,
  };
  protected loading = true;
  protected saving = false;
  protected message = '';
  protected error = '';

  ngOnInit(): void {
    this.load();
  }

  protected load(): void {
    this.loading = true;
    this.error = '';
    this.message = '';

    this.goalsService.get().subscribe({
      next: (goalSettings) => {
        this.goalSettings = goalSettings;
        this.loading = false;
      },
      error: () => {
        this.error = 'Unable to load goal settings.';
        this.loading = false;
      },
    });
  }

  protected save(): void {
    this.saving = true;
    this.error = '';
    this.message = '';

    this.goalsService.update(this.goalSettings).subscribe({
      next: (goalSettings) => {
        this.goalSettings = goalSettings;
        this.message = 'Goal settings updated.';
        this.saving = false;
      },
      error: () => {
        this.error = 'Unable to update goal settings.';
        this.saving = false;
      },
    });
  }
}
