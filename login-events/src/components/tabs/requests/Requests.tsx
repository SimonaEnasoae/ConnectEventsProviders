import { FC } from "react";
import { useState, useEffect } from "react";
import Table from "../../utils/table/Table";
import Pagination from "../../utils/pagination/Pagination";
import "./Requests.css";
import { RootState } from "../../auth/state-management/rootReducer";
import { useSelector } from "react-redux";
import {
  getRequests,
  formatRawData,
  columns,
  declineRequest,
} from "../../utils/middleware/requestsServiceApi";
import { showAlert } from "../../utils/popup/ShowAlert";
import { RequestModel } from "../../../models/RequestModel";

const Requests: FC = () => {
  const user = useSelector((state: RootState) => state.user.user);

  const [pageData, setPageData] = useState({
    rowData: [],
    isLoading: false,
    totalPages: 0,
    totalRequests: 20,
  });

  const [currentPage, setCurrentPage] = useState(1);
  useEffect(() => {
    setPageData((prevState) => ({
      ...prevState,
      rowData: [],
      isLoading: true,
    }));

    getRequests(user?.Id, 9, currentPage - 1).then((info) => {
      const { totalPages, totalRequests, data } = info;
      setPageData({
        isLoading: false,
        rowData: formatRawData(data),
        totalPages,
        totalRequests: 20,
      });
    });
  }, [currentPage]);

  function updateStatusDecline(event: any) {
    const requestId = event.target.attributes["request-key"]?.value;
    const request = {
      Id: requestId,
      Status: 2,
    };
    console.log(request);
    declineRequest(request).then((response) => {
      showAlert("Request declined", "success");
    });
  }

  function displayRequest(cells: any) {
    return (
      //   <div key={cells[0].value}>
      // {/* {console.log(cells)} */}
      <tr key={cells[0].value}>
        <td>
          {console.log(cells)}
          <div className="flex-container">
            <div className="flex-child">
              <img
                className="imgEvent1"
                src={`data:image/png;base64,${cells[1].value}`}
              ></img>
              <p>{cells[3].value}</p>
              <p>
                {cells[4].value} - {cells[5].value}
              </p>
            </div>

            <div className="flex-child1">
              <img
                className="imgProvider1"
                src={`data:image/png;base64,${cells[2].value}`}
              ></img>
              <p>{cells[6].value}</p>
              <button
                className="btn btn-primary pendingStatus"
                // onClick={sendRequest}
              >
                {cells[7].value}
              </button>
              <button
                request-key={cells[0].value}
                className="btn btn-primary acceptBtn"
                onClick={updateStatusDecline}
              >
                Accept
              </button>
              <button
                request-key={cells[0].value}
                className="btn btn-primary declineBtn"
                onClick={updateStatusDecline}
              >
                Decline
              </button>
            </div>
          </div>
        </td>
      </tr>
    );
  }

  return (
    <div>
      <div style={{ height: "80%" }}>
        <Table
          displayEvent={displayRequest}
          columns={columns}
          data={pageData.rowData}
          isLoading={pageData.isLoading}
        />
        <div>
          <Pagination
            totalRows={pageData.totalRequests}
            pageChangeHandler={setCurrentPage}
            rowsPerPage={9}
          />
        </div>
      </div>
    </div>
  );
};

export default Requests;
