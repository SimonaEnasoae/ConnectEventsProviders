import React, { FC } from "react";
import { Button } from "react-bootstrap";
import { useAppDispatch } from "../../auth/state-management/store";
import { showAlert } from "../../utils/popup/ShowAlert";
import { clearToken, setAuthState } from "../../auth/authSlice";
import { useSelector } from "react-redux";
import { RootState } from "../../auth/state-management/rootReducer";

const Logout: FC = () => {
  const dispatch = useAppDispatch();
  // const { user } = useSelector((state) => state.user);
  const user = useSelector((state: RootState) => state.user.user);
  return (
    <div>
      <h2>Have a nice day!</h2>
      <p> {user?.Type}</p>
      <p> {user?.Username}</p>
      <Button
        style={{ width: "100px", margin: "10px" }}
        onClick={() => {
          dispatch(clearToken());
          dispatch(setAuthState(false));
        }}
      >
        Log Out
      </Button>
    </div>
  );
};

export default Logout;
