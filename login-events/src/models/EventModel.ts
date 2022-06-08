export type EventModel = {
  Id: string;
  Title: string;
  Description: string;
  Location: string;
  EndDate: Date | null;
  StartDate: Date | null;
  Tags: string[];
  organiserId?: string;
};
