export type RequestModel = {
  Id?: string;
  senderId: string;
  receiverId: string;
  eventId: string;
  token?: string | null;
};
