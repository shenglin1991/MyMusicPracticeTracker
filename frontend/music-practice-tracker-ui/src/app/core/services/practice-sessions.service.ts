import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  PracticeSession,
  PracticeSessionFilters,
  PracticeSessionUpsertRequest,
} from '../models/practice-session.model';

@Injectable({ providedIn: 'root' })
export class PracticeSessionsService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = '/api/practice-sessions';

  list(filters: PracticeSessionFilters = {}): Observable<PracticeSession[]> {
    let params = new HttpParams();

    if (filters.instrumentId) {
      params = params.set('instrumentId', filters.instrumentId);
    }

    if (filters.from) {
      params = params.set('from', filters.from);
    }

    if (filters.to) {
      params = params.set('to', filters.to);
    }

    return this.http.get<PracticeSession[]>(this.baseUrl, { params });
  }

  create(payload: PracticeSessionUpsertRequest): Observable<PracticeSession> {
    return this.http.post<PracticeSession>(this.baseUrl, payload);
  }

  update(id: string, payload: PracticeSessionUpsertRequest): Observable<PracticeSession> {
    return this.http.put<PracticeSession>(`${this.baseUrl}/${id}`, payload);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
