import { parseISO, format } from "date-fns";

export const columns = [
  {
    Header: "Id",
    accessor: "id",
  },
  {
    Header: "Title",
    accessor: "title",
  },
  {
    Header: "OrganiserId",
    accessor: "organiserId",
  },
  {
    Header: "Location",
    accessor: "location",
  },
  {
    Header: "Start Date",
    accessor: "startDate",
  },
  {
    Header: "End date",
    accessor: "endDate",
  },
  {
    Header: "Tags",
    accessor: "tags",
  },
  {
    Header: "Image",
    accessor: "image",
  },
];
export const UrlEventsBase = "https://localhost:5003/api/events";
export const UrlUpdateEventsBase = "https://localhost:5009/api/events";

export const formatRawData = (rawData) =>
  rawData.map((info) => ({
    ...info,
    startDate: format(parseISO(info.startDate), "MM/dd/yyyy"),
    endDate: format(parseISO(info.endDate), "MM/dd/yyyy"),
  }));

export const getEvents = async (
  organiserId = "",
  pageSize = 9,
  pageIndex = 0
) => {
  const response = await fetch(
    // `https://api.instantwebtools.net/v1/passenger?page=${pageNo}&size=15`
    `${UrlEventsBase}?organiserId=${organiserId}&pageSize=${pageSize}&pageIndex=${pageIndex}`
  );
  const data = await response.json();
  return data;
};

export const saveEvent = async (event1) => {
  const response = await fetch(UrlUpdateEventsBase, {
    method: "POST",
    headers: { "Content-type": "application/json" },
    body: JSON.stringify(event1),
    // body: JSON.stringify(event1)
  });
  const data = await response.json();
  console.log(data);
  return data;
};

export const savePictureEvent = async (formData) => {
  fetch(`${UrlUpdateEventsBase}/file`, {
    method: "POST",
    body: formData,
  })
    .then((r) => r.json())
    .then((data) => {
      console.log(data);
    });
};
