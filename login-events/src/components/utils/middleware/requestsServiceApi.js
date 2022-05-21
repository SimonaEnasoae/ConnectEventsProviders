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
