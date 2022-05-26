import { FC } from "react";
import { useState, useEffect } from "react";
import {
  getEvents,
  columns,
  formatRawData,
} from "../../utils/middleware/eventsServiceApi";
import Table from "../../utils/table/Table";
import Pagination from "../../utils/pagination/Pagination";
import "./Events.css";
import { RootState } from "../../auth/state-management/rootReducer";
import { useSelector } from "react-redux";
import { createRequest } from "../../utils/middleware/requestsServiceApi";
import { showAlert } from "../../utils/popup/ShowAlert";
import { RequestModel } from "../../../models/RequestModel";

const Events: FC = () => {
  const user = useSelector((state: RootState) => state.user.user);

  const [pageData, setPageData] = useState({
    rowData: [],
    isLoading: false,
    totalPages: 0,
    totalEvents: 20,
  });

  const [currentPage, setCurrentPage] = useState(1);
  useEffect(() => {
    setPageData((prevState) => ({
      ...prevState,
      rowData: [],
      isLoading: true,
    }));

    getEvents("", 9, currentPage - 1).then((info) => {
      const { totalPages, totalEvents, data } = info;
      setPageData({
        isLoading: false,
        rowData: formatRawData(data),
        totalPages: totalPages,
        totalEvents: totalEvents,
      });
    });
  }, [currentPage]);

  function sendRequest(event: any) {
    const currentEventId = event.target.attributes["event-key"]?.value;
    const currentOrganiserId = event.target.attributes["organiser-key"]?.value;

    const providerId = user?.Id ?? "";
    const request: RequestModel = {
      senderId: providerId,
      receiverId: currentOrganiserId,
      eventId: currentEventId,
    };
    console.log(request);
    createRequest(request).then((response) => {
      showAlert("Request created", "success");
    });
  }
  function displayEvent(cells: any) {
    return (
      <tr key={cells[0].value}>
        <td>
          {/* {console.log(cells)} */}
          <div className="col-sm-4 ">
            <div>
              <img
                className="imgEvent"
                src={`data:image/png;base64,${cells[7].value}`}
              ></img>
              <div className="boxInfo">
                <p className="titleEvent"> {cells[1].value} </p>
                <p> {cells[3].value}</p>
                <p>
                  {" "}
                  {cells[4].value}-{cells[5].value}
                </p>
                {cells[6].value.map((tag) => {
                  return (
                    <button key={tag.id} className="tag btn btn-info">
                      {" "}
                      {tag.value}
                    </button>
                  );
                })}
                {user?.Type === "Provider" && (
                  <p>
                    <button
                      event-key={cells[0].value}
                      organiser-key={cells[2].value}
                      className="btn btn-primary requestBtn"
                      onClick={sendRequest}
                    >
                      {" "}
                      Request
                    </button>
                  </p>
                )}
              </div>
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
          displayEvent={displayEvent}
          columns={columns}
          data={pageData.rowData}
          isLoading={pageData.isLoading}
        />
        <div>
          <Pagination
            totalRows={pageData.totalEvents}
            pageChangeHandler={setCurrentPage}
            rowsPerPage={9}
          />
        </div>
      </div>
    </div>
  );
};

export default Events;
