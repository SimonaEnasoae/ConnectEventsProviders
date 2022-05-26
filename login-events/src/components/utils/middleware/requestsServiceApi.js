import { parseISO, format } from "date-fns";
var moment = require("moment"); // require

export const UrlRequestsBase = "https://localhost:5009/api/";

export const createRequest = async (request) => {
  const response = await fetch(`${UrlRequestsBase}requests`, {
    method: "POST",
    headers: { "Content-type": "application/json" },
    body: JSON.stringify(request),
    // body: JSON.stringify(event1)
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
    // ...info,
    // image: format(parseISO(info.startDate), "MM/dd/yyyy"),
    // endDate: format(parseISO(info.endDate), "MM/dd/yyyy"),
    // // tags: info.tags.map(e=>e.value).join(" "),
    Id: info.id,
    ImageEvent: info.eventModel.image,
    ImageProvider: info.provider.image,
    TitleEvent: info.eventModel.title,
    EndDate: moment.utc(info.eventModel.endDate).format("MM/DD/YYYY"),
    StartDate: moment.utc(info.eventModel.startDate).format("MM/DD/YYYY"),
    Status: info.status,
    Tag: info.provider.tag,

    // endDate: format(parseISO(info.endDate), "MM/dd/yyyy"),
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
    // body: JSON.stringify(event1)
  });
  const data = await response.json();
  return data;
};
