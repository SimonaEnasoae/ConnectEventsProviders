export type EventModel = {
  Id: string;
  Title: string;
  Description: string;
  Location: string;
  EndDate: Date;
  StartDate: Date;
  Tags: string[];
  // createdAt?: string;
  // updatedAt?: string;
  organiserId?: string;
};
