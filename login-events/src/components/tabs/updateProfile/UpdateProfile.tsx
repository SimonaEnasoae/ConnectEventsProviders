import React, { FC, useEffect, useState } from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import { Form, Button } from "react-bootstrap";
import "./UpdateProfile.css";
import { Controller, useForm } from "react-hook-form";
import {
  updateProfile,
  savePictureProvider,
  getProvider,
} from "../../utils/middleware/providersServiceApi";
import { showAlert } from "../../utils/popup/ShowAlert";
import { Provider } from "../../../models/Provider";
import { RootState } from "../../auth/state-management/rootReducer";
import { useSelector } from "react-redux";
import { setDate } from "date-fns";
import Loader from "../../utils/loader/Loader";

const emptyProvider: Provider = {
  id: "",
  title: "",
  description: "",
  tag: "",
  location: "",
  image: "",
};

const UpdateProfile = () => {
  const [provider, setProvider] = useState(emptyProvider);
  const [file, setFile] = useState("");
  const [fileName, setFileName] = useState("");
  const [isLoading, setiIsLoading] = useState(true);

  const [selectedTags, setSelectedTags] = useState<string[]>([]);
  const [imageSrc, setImageSrc] = useState(emptyProvider.image);
  const user = useSelector((state: RootState) => state.user.user);

  useEffect(() => {
    const fetchProvider = async () => {
      const pr = await getProvider(user?.Id);
      console.log(pr);
      setProvider(pr);
      setImageSrc(`data:image/png;base64,${pr.image}`);
      setiIsLoading(false);
    };
    fetchProvider();
  }, []);

  const saveFile = async (e) => {
    setFile(e.target.files[0]);
    setFileName(e.target.files[0].name);
    setImageSrc(URL.createObjectURL(e.target.files[0]));
  };

  const uploadFile = async (eventId) => {
    console.log(file);
    const formData = new FormData();
    formData.append("formFile", file);
    formData.append("fileName", provider.title);
    formData.append("providerId", eventId);
    savePictureProvider(formData);
    showAlert("Profile Updated", "success");
  };

  async function handleSubmit(e) {
    e.preventDefault();
    updateProfile(provider).then((eventObject) => {
      if (file != "") {
        uploadFile(eventObject.id);
      } else {
        showAlert("Profile Updated", "success");
      }
    });
  }

  function updateSelectedTags(e) {
    console.log(e.target.id);
    setProvider({
      ...provider,
      tag: e.target.id,
    });
  }

  // fetchProvider();

  return (
    <>
      {isLoading ? (
        <Loader />
      ) : (
        <>
          {console.log("render", provider)}
          <h1 className="titleEvent">Update Profile Provider</h1>
          <div className="form-container">
            <Form onSubmit={handleSubmit}>
              <div className="col-md-12 inputDetails">
                <label className="labelTitle">Title</label>
                <input
                  className="name-input"
                  type="text"
                  value={provider.title}
                  name="Title"
                  onChange={(e) => {
                    console.log(e.target.value);
                    setProvider({
                      ...provider,
                      title: e.target.value,
                    });
                  }}
                />
              </div>

              <div className="col-md-12 inputDetails">
                <label className="labelLocation">Location</label>
                <input
                  className="name-input"
                  type="text"
                  value={provider.location}
                  onChange={(e) => {
                    console.log(e.target.value);
                    setProvider({
                      ...provider,
                      location: e.target.value,
                    });
                  }}
                />
              </div>

              <div className="col-md-12 inputDetails">
                <label className="labelDescription">Description</label>

                <input
                  className="name-input"
                  type="text"
                  value={provider.description}
                  name="Description"
                  onChange={(e) => {
                    console.log(e.target.value);
                    setProvider({
                      ...provider,
                      description: e.target.value,
                    });
                  }}
                />
              </div>
              <div className="tagsContainer">
                <p>
                  <strong>Choose tag</strong>{" "}
                </p>

                <label className="task">
                  <input
                    type="checkbox"
                    name=""
                    id="Food"
                    onClick={updateSelectedTags}
                    defaultChecked={provider.tag === "Food"}
                  />
                  Food
                </label>
                <label className="task">
                  <input
                    type="checkbox"
                    name=""
                    id="Clothes"
                    onClick={updateSelectedTags}
                    defaultChecked={provider.tag === "Clothes"}
                  />
                  Clothes
                </label>
                <label className="task">
                  <input
                    type="checkbox"
                    name=""
                    id="Game"
                    onClick={updateSelectedTags}
                    defaultChecked={provider.tag === "Game"}
                  />
                  Game
                </label>
                <label className="task">
                  <input
                    type="checkbox"
                    name=""
                    id="Other"
                    onClick={updateSelectedTags}
                    defaultChecked={provider.tag === "Other"}
                  />
                  Other
                </label>
              </div>
              {/* </div> */}

              <p>
                <strong> Image</strong>
              </p>

              <img
                className="imgUpdateProfile"
                // src={URL.createObjectURL(provider.image)}
                src={imageSrc}
              ></img>

              <input
                className="imageInput"
                type="file"
                onChange={saveFile}
              ></input>
              {/* <input className="uploadFileButton" type="button" value="upload" onClick={uploadFile}></input> */}

              <Button className="submit-button" value="submit" type="submit">
                Update
              </Button>
            </Form>
          </div>
        </>
      )}
    </>
  );
};

export default UpdateProfile;
