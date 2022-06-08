import React, { FC, useState, useEffect } from "react";
import "./pagination.css";

const Pagination = ({ pageChangeHandler, totalRows, rowsPerPage }) => {
  const noOfPages = Math.ceil(totalRows / rowsPerPage);

  const pagesArr = [...new Array(noOfPages)];

  const [currentPage, setCurrentPage] = useState(1);

  const [canGoBack, setCanGoBack] = useState(false);
  const [canGoNext, setCanGoNext] = useState(true);

  const [pageFirstRecord, setPageFirstRecord] = useState(1);
  const [pageLastRecord, setPageLastRecord] = useState(rowsPerPage);

  const onNextPage = () => setCurrentPage(currentPage + 1);
  const onPrevPage = () => setCurrentPage(currentPage - 1);
  const onPageSelect = (pageNo: React.SetStateAction<number>) =>
    setCurrentPage(pageNo);

  useEffect(() => {
    if (noOfPages === currentPage) {
      setCanGoNext(false);
    } else {
      setCanGoNext(true);
    }
    if (currentPage === 1) {
      setCanGoBack(false);
    } else {
      setCanGoBack(true);
    }
  }, [noOfPages, currentPage]);

  useEffect(() => {
    const skipFactor = (currentPage - 1) * rowsPerPage;
    pageChangeHandler(currentPage);
    setPageFirstRecord(skipFactor + 1);
  }, [currentPage]);

  useEffect(() => {
    const count = pageFirstRecord + rowsPerPage;
    setPageLastRecord(count > totalRows ? totalRows : count - 1);
  }, [pageFirstRecord, rowsPerPage, totalRows]);

  return (
    <>
      {noOfPages > 1 ? (
        <div className={"pagination"}>
          <div className={"pageInfo"}>
            Showing {pageFirstRecord} - {pageLastRecord} of {totalRows}
          </div>
          <div className={"pagebuttons"}>
            <button
              className={"pageBtn"}
              onClick={onPrevPage}
              disabled={!canGoBack}
            ></button>
            {pagesArr.map((num, index) => (
              <button
                key={index}
                onClick={() => onPageSelect(index + 1)}
                className={` pageBtn  ${
                  index + 1 === currentPage ? "activeBtn" : ""
                }`}
              >
                {index + 1}
              </button>
            ))}
            <button
              className={"pageBtn"}
              onClick={onNextPage}
              disabled={!canGoNext}
            ></button>
          </div>
        </div>
      ) : null}
    </>
  );
};

export default Pagination;
