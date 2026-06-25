import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GoalSettings } from '../models/goal-settings.model';

@Injectable({ providedIn: 'root' })
export class GoalsService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = '/api/goals';

  get(): Observable<GoalSettings> {
    return this.http.get<GoalSettings>(this.baseUrl);
  }

  update(payload: GoalSettings): Observable<GoalSettings> {
    return this.http.put<GoalSettings>(this.baseUrl, payload);
  }
}
