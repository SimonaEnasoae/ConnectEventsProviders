import React, { FC, useEffect, useState } from "react";
import {
  getProviders,
  columns,
} from "../../utils/middleware/providersServiceApi";
import Table from "../../utils/table/Table";
import Pagination from "../../utils/pagination/Pagination";

import "./Providers.css";
const Providers: FC = () => {
  const [pageData, setPageData] = useState({
    rowData: [],
    isLoading: false,
    totalPages: 0,
    totalProviders: 20,
  });
  const [currentPage, setCurrentPage] = useState(1);
  useEffect(() => {
    setPageData((prevState) => ({
      ...prevState,
      rowData: [],
      isLoading: true,
    }));

    getProviders(9, currentPage - 1).then((info) => {
      const { totalPages, totalProviders, data } = info;
      setPageData({
        isLoading: false,
        rowData: data,
        totalPages: totalPages,
        totalProviders: totalProviders,
      });
    });
  }, [currentPage]);

  function displayProvider(cells: any) {
    return (
      <tr key={cells[0].value}>
        <td>
          <div className="col-sm-4 wrapper">
            <div className="box info">
              <img
                className="imgProvider"
                src={`data:image/png;base64,${cells[4].value}`}
              ></img>
              {/* {console.log(cells)} */}
              <div>
                <p className="titleProvider"> {cells[0].value} </p>
                <p className="locationProvider"> {cells[2].value}</p>
                <button className="tag tagProvider btn-primary">
                  {" "}
                  {cells[3].value}
                </button>
              </div>
            </div>
          </div>
        </td>
      </tr>
    );
  }

  return (
    <div>
      <div>
        <Table
          displayEvent={displayProvider}
          columns={columns}
          data={pageData.rowData}
          isLoading={pageData.isLoading}
        />

        <Pagination
          totalRows={pageData.totalProviders}
          pageChangeHandler={setCurrentPage}
          rowsPerPage={9}
        />
      </div>
    </div>
  );
};

export default Providers;
