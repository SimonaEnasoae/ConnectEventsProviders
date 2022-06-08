import React, { FC, useEffect, useState } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Form, Button } from "react-bootstrap";
import "./CreateEvent.css";
import { Controller, useForm } from "react-hook-form";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { EventModel } from "../../../models/EventModel";
import {
  saveEvent,
  savePictureEvent,
} from "../../utils/middleware/eventsServiceApi";
import { showAlert } from "../../utils/popup/ShowAlert";
import { RootState } from "../../auth/state-management/rootReducer";
import { useSelector } from "react-redux";

const emptyEvent: EventModel = {
  Id: "",
  Title: "",
  Description: "",
  Tags: [],
  Location: "",
  EndDate: new Date(),
  StartDate: new Date(),
};

const CreateEvent = () => {
  const [event, setEvent] = useState(emptyEvent);
  const [file, setFile] = useState("");
  const [fileName, setFileName] = useState("");
  const [isActive, setActive] = useState(false);
  const [selectedTags, setSelectedTags] = useState<string[]>([]);
  const user = useSelector((state: RootState) => state.user.user);

  const saveFile = (e) => {
    console.log(e.target.files[0]);
    setFile(e.target.files[0]);
    setFileName(e.target.files[0].name);
  };

  const uploadFile = async (eventId) => {
    console.log(file);
    const formData = new FormData();
    formData.append("formFile", file);
    formData.append("fileName", event.Title);
    formData.append("eventId", eventId);
    savePictureEvent(formData);
    showAlert("Event Created", "success");
  };

  async function handleSubmit(e) {
    e.preventDefault();
    var newEvent = { ...event, Tags: selectedTags, organiserId: user?.Id };
    saveEvent(newEvent).then((eventObject) => {
      console.log(eventObject);
      uploadFile(eventObject.id);
    });
  }

  function updateSelectedTags(e) {
    var tag = e.target.id;
    if (e.target.checked) {
      if (selectedTags.indexOf(tag) <= -1) {
        selectedTags.push(tag);
      } else {
        var newTags = selectedTags.filter((item) => item !== tag);
        setSelectedTags(newTags);
      }
    }
  }

  return (
    <>
      <h1 className="titleEvent">Create Event</h1>
      <div className="form-container">
        <Form onSubmit={handleSubmit}>
          <div className="col-md-6">
            <input
              className="name-input"
              type="text"
              placeholder="Title"
              name="Title"
              onChange={(e) => {
                console.log(e.target.value);
                setEvent({
                  ...event,
                  Title: e.target.value,
                });
              }}
            />
          </div>

          <div className="col-md-6">
            <input
              className="name-input"
              type="text"
              placeholder="Location"
              name="Location"
              onChange={(e) => {
                console.log(e.target.value);
                setEvent({
                  ...event,
                  Location: e.target.value,
                });
              }}
            />
          </div>

          <div className="col-md-12">
            <input
              className="name-input"
              type="text"
              placeholder="Description"
              name="Description"
              onChange={(e) => {
                console.log(e.target.value);
                setEvent({
                  ...event,
                  Description: e.target.value,
                });
              }}
            />
          </div>
          <div className="col-md-6">
            <p>
              <strong>Start Date</strong>
            </p>
            <DatePicker
              placeholderText="Start date"
              onChange={(date) => {
                console.log("update start date", date);
                setEvent({
                  ...event,
                  StartDate: date,
                });
              }}
              showTimeSelect
              timeFormat="HH:mm"
              timeIntervals={15}
              timeCaption="time"
              dateFormat="MM-dd-yyyy h:mm"
              selected={event.StartDate}
            />
          </div>

          <div className="col-md-6">
            <p>
              <strong>End Date</strong>
            </p>
            <DatePicker
              placeholderText="End date"
              onChange={(date) => {
                console.log("update date", date);
                setEvent({
                  ...event,
                  EndDate: date,
                });
              }}
              showTimeSelect
              timeFormat="HH:mm"
              timeIntervals={15}
              timeCaption="time"
              dateFormat="MM-dd-yyyy h:mm"
              selected={event.EndDate}
            />
          </div>
          <p>
            <strong>Choose tags</strong>{" "}
          </p>

          <label className="task">
            <input
              type="checkbox"
              name=""
              id="Food"
              onClick={updateSelectedTags}
            />
            Food
          </label>
          <label className="task">
            <input
              type="checkbox"
              name=""
              id="Clothes"
              onClick={updateSelectedTags}
            />
            Clothes
          </label>
          <label className="task">
            <input
              type="checkbox"
              name=""
              id="Game"
              onClick={updateSelectedTags}
            />
            Game
          </label>
          <label className="task">
            <input
              type="checkbox"
              name=""
              id="Other"
              onClick={updateSelectedTags}
            />
            Other
          </label>
          {/* </div> */}

          <p>
            <strong> Image</strong>
          </p>
          <input className="imageInput" type="file" onChange={saveFile}></input>
          {/* <input className="uploadFileButton" type="button" value="upload" onClick={uploadFile}></input> */}

          <Button className="submit-button" value="submit" type="submit">
            Create Event
          </Button>
        </Form>
      </div>
    </>
  );
};

export default CreateEvent;
