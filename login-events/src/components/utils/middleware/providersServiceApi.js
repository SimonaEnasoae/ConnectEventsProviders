import { parseISO, format } from "date-fns";

export const columns = [
  {
    Header: "title",
    accessor: "title",
  },
  {
    Header: "description",
    accessor: "description",
  },
  {
    Header: "location",
    accessor: "location",
  },
  {
    Header: "tag",
    accessor: "tag",
  },
  {
    Header: "image",
    accessor: "image",
  },
];
export const UrlProvidersBase = "https://localhost:5005/api/";
export const UrlUpdateProvidersBase = "https://localhost:5009/api/";

export const getProviders = async (pageSize = 9, pageIndex = 0) => {
  const response = await fetch(
    // `https://api.instantwebtools.net/v1/passenger?page=${pageNo}&size=15`
    `${UrlProvidersBase}providers?pageSize=${pageSize}&pageIndex=${pageIndex}`
  );
  const data = await response.json();
  return data;
};

export const updateProfile = async (provider) => {
  const response = await fetch(`${UrlUpdateProvidersBase}providers`, {
    method: "POST",
    headers: { "Content-type": "application/json" },
    body: JSON.stringify(provider),
  });
  const data = await response.json();
  return data;
};

export const savePictureProvider = async (formData) => {
  fetch(`${UrlUpdateProvidersBase}providers/file`, {
    method: "POST",
    body: formData,
  })
    .then((r) => r.json())
    .then((data) => {
      console.log(data);
    });
};

export const getProvider = async (providerId) => {
  const response = await fetch(
    `${UrlProvidersBase}provider?providerId=${providerId}`
  );
  const data = await response.json();
  return data;
};
