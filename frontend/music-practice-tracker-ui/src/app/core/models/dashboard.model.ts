export interface DashboardSummary {
  todayMinutes: number;
  weekMinutes: number;
  monthMinutes: number;
  totalMinutes: number;
  breakdownByInstrument: InstrumentBreakdown[];
  goals: GoalProgress;
}

export interface InstrumentBreakdown {
  instrumentId: string;
  instrumentName: string;
  instrumentColor: string;
  totalMinutes: number;
}

export interface GoalProgress {
  dailyTargetMinutes: number;
  weeklyTargetMinutes: number;
  todayProgressMinutes: number;
  weekProgressMinutes: number;
  dailyCompletionPercentage: number;
  weeklyCompletionPercentage: number;
}
