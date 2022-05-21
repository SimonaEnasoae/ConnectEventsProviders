import React, { FC, lazy, Suspense } from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import { useSelector } from "react-redux";
import { RootState } from "../../components/auth/state-management/rootReducer";
import { useAppDispatch } from "../../components/auth/state-management/store";
import { setAuthState } from "../../components/auth/authSlice";

const Auth = lazy(() => import("../../components/auth/Auth"));
const Home = lazy(() => import("../home/Home"));

const App: FC = () => {
  const dispatch = useAppDispatch();

  var wasLoggedIn = localStorage.getItem("isLoggedIn") === "true";
  wasLoggedIn = wasLoggedIn ? wasLoggedIn : false;
  //  dispatch(setAuthState(wasLoggedIn));

  const isLoggedIn = useSelector(
    (state: RootState) => state.auth.isAuthenticated
  );
  return (
    <Router>
      <Switch>
        <Route path="/">
          <Suspense fallback={<p>Loading...</p>}>
            {isLoggedIn || wasLoggedIn ? <Home /> : <Auth />}
          </Suspense>
        </Route>
      </Switch>
    </Router>
  );
};

export default App;
