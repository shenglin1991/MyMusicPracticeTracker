export interface Instrument {
  id: string;
  name: string;
  color: string;
  createdAt: string;
}

export interface InstrumentUpsertRequest {
  name: string;
  color: string;
}
