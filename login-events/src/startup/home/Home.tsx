import React, { FC, lazy } from "react";
import {
  Navbar,
  NavDropdown,
  Form,
  FormControl,
  Button,
  Nav,
} from "react-bootstrap";
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link,
  NavLink,
} from "react-router-dom";
import Logout from "../../components/tabs/logout/Logout";
import CreateEvent from "../../components/tabs/createEvent/CreateEvent";
import Events from "../../components/tabs/events/Events";
import MyEvents from "../../components/tabs/myEvents/MyEvents";
import Providers from "../../components/tabs/providers/Providers";
import "./Home.css";
import UpdateProfile from "../../components/tabs/updateProfile/UpdateProfile";
import { RootState } from "../../components/auth/state-management/rootReducer";
import { useSelector } from "react-redux";
import Requests from "../../components/tabs/requests/Requests";
const Home: FC = () => {
  const user = useSelector((state: RootState) => state.user.user);

  return (
    <Router>
      <div className="top-menu-bar">
        <NavLink className={"navlink"} to="/events">
          Events
        </NavLink>
        {user?.Type === "EventHost" && (
          <NavLink className={"navlink"} to="/createEvent">
            Create Event
          </NavLink>
        )}
        {user?.Type === "EventHost" && (
          <NavLink className={"navlink"} to="/myEvents">
            My Events
          </NavLink>
        )}
        <NavLink className={"navlink"} to="/providers">
          Providers
        </NavLink>
        {user?.Type === "Provider" && (
          <NavLink className={"navlink"} to="/updateProfile">
            Update Profile
          </NavLink>
        )}
        <NavLink className={"navlink"} to="/requests">
          Requests
        </NavLink>

        <NavLink className={"navlink"} to="/logout">
          Logout
        </NavLink>
      </div>
      <div>
        <Switch>
          <Route path="/events">
            <Events />
          </Route>

          <Route path="/createEvent">
            <CreateEvent />
          </Route>

          <Route path="/myEvents">
            <MyEvents />
          </Route>

          <Route path="/providers">
            <Providers />
          </Route>
          <Route path="/updateProfile">
            <UpdateProfile />
          </Route>
          <Route path="/requests">
            <Requests />
          </Route>
          <Route path="/logout">
            <Logout />
          </Route>
        </Switch>
      </div>
    </Router>
  );
};

export default Home;
