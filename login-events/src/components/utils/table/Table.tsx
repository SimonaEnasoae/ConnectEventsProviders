import React, { useMemo } from "react";
import { useTable } from "react-table";
import Loader from "../loader/Loader";
import "./table.css";

const AppTable = ({
  displayEvent,
  columns,
  data,
  isLoading,
  classTable = "",
}) => {
  const columnData = useMemo(() => columns, [columns]);
  const rowData = useMemo(() => data, [data]);
  const { getTableProps, getTableBodyProps, headerGroups, rows, prepareRow } =
    useTable({
      columns: columnData,
      data: rowData,
    });

  return (
    <>
      {isLoading ? (
        <Loader />
      ) : (
        <>
          <table {...getTableProps()} className={classTable}>
            <tbody {...getTableBodyProps()}>
              {rows.map((row, i) => {
                prepareRow(row);
                return displayEvent(row.cells);
              })}
            </tbody>
          </table>
        </>
      )}
    </>
  );
};

export default AppTable;
