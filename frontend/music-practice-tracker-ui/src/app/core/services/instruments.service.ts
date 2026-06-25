import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Instrument, InstrumentUpsertRequest } from '../models/instrument.model';

@Injectable({ providedIn: 'root' })
export class InstrumentsService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = '/api/instruments';

  list(): Observable<Instrument[]> {
    return this.http.get<Instrument[]>(this.baseUrl);
  }

  create(payload: InstrumentUpsertRequest): Observable<Instrument> {
    return this.http.post<Instrument>(this.baseUrl, payload);
  }

  update(id: string, payload: InstrumentUpsertRequest): Observable<Instrument> {
    return this.http.put<Instrument>(`${this.baseUrl}/${id}`, payload);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
