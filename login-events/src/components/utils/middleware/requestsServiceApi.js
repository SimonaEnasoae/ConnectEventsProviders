import { parseISO, format } from "date-fns";
var moment = require("moment"); // require

export const UrlRequestsBase = "http://localhost:5009/api/";

export const createRequest = async (request) => {
  const response = await fetch(`${UrlRequestsBase}requests`, {
    method: "POST",
    headers: { "Content-type": "application/json" },
    body: JSON.stringify(request),
  });
  const data = await response.json();
  return data;
};

export const columns = [
  {
    Header: "Id",
    accessor: "Id",
  },
  {
    Header: "ImageEvent",
    accessor: "ImageEvent",
  },
  {
    Header: "ImageProvider",
    accessor: "ImageProvider",
  },
  {
    Header: "TitleEvent",
    accessor: "TitleEvent",
  },
  {
    Header: "EndDate",
    accessor: "EndDate",
  },
  {
    Header: "StartDate",
    accessor: "StartDate",
  },
  {
    Header: "Tag",
    accessor: "Tag",
  },
  {
    Header: "Status",
    accessor: "Status",
  },
];
export const formatRawData = (rawData) => {
  console.log(rawData);
  return rawData.map((info) => ({
    Id: info.id,
    ImageEvent: info.eventModel.image,
    ImageProvider: info.provider.image,
    TitleEvent: info.eventModel.title,
    EndDate: moment.utc(info.eventModel.endDate).format("MM/DD/YYYY"),
    StartDate: moment.utc(info.eventModel.startDate).format("MM/DD/YYYY"),
    Status: info.status,
    Tag: info.provider.tag,
  }));
};

export const getRequests = async (userId = "", pageSize = 6, pageIndex = 0) => {
  const response = await fetch(
    `${UrlRequestsBase}requests?userId=${userId}&pageSize=${pageSize}&pageIndex=${pageIndex}`
  );
  const data = await response.json();
  return data;
};

export const declineRequest = async (request) => {
  const response = await fetch(`${UrlRequestsBase}requests/status`, {
    method: "POST",
    headers: { "Content-type": "application/json" },
    body: JSON.stringify(request),
  });
  const data = await response.json();
  return data;
};
