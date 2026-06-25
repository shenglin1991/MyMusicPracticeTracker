export interface PracticeSession {
  id: string;
  instrumentId: string;
  instrumentName: string;
  instrumentColor: string;
  startTime: string;
  endTime: string;
  durationMinutes: number;
  notes?: string | null;
  createdAt: string;
}

export interface PracticeSessionUpsertRequest {
  instrumentId: string;
  startTime: string;
  endTime: string;
  notes?: string | null;
}

export interface PracticeSessionFilters {
  instrumentId?: string;
  from?: string;
  to?: string;
}
