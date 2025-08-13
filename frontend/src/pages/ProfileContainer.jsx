import React, { useState, useEffect } from "react";
import Form from "../components/shared/Form";

import { useSnackbar } from "notistack";
import { useContext } from "react";
import { AppContext } from "../AppContext";
const ProfileContainer = () => {
  const [profileForm, setProfileForm] = useState({
    firstname: "",
    lastname: "",
    email: "",
    phonenumber: "",
    currjobtitle: "",
    industry: "",
    yearsofexp: "",
    educationalbg: "",
    desiredjob: "",
    cv: ""
  });

  const { enqueueSnackbar } = useSnackbar();
  const userId = localStorage.getItem("userId");

  const { displayNavbar, setUsername, userType } = useContext(AppContext);
  const handleChange = (event, name) => {
    const value = event.target.value;
    setProfileForm((prevState) => ({ ...prevState, [name]: value }));
  };

  const fetchData = async () => {
    const response = await fetch(`https://localhost:7000/user/${userId}`)
    const profile = await response.json();

    const {
      firstname,
      lastname,
      email,
      phonenumber,
      currjobtitle,
      industry,
      yearsofexp,
      educationalbg,
      desiredjob,
      cv
    } = profile;
    if (
      firstname &&
      lastname &&
      email &&
      phonenumber &&
      currjobtitle &&
      industry &&
      yearsofexp &&
      educationalbg &&
      desiredjob
    ) {
      setProfileForm({
        firstname: firstname,
        lastname: lastname,
        email: email,
        phonenumber: phonenumber,
        currjobtitle: currjobtitle,
        industry: industry,
        yearsofexp: yearsofexp,
        educationalbg: educationalbg,
        desiredjob: desiredjob,
        cv: cv
      });
    } else if (firstname && lastname && email) {
      setProfileForm({
        firstname: firstname,
        lastname: lastname,
        email: email,
      });
    }

    // setProfileForm({
    //   firstname: firstname || "",
    //   lastname: lastname || "",
    //   email: email || "",
    //   phonenumber: phonenumber || "",
    //   currjobtitle: currjobtitle || "",
    //   industry: industry || "",
    //   yearsofexp: yearsofexp || "",
    //   educationalbg: educationalbg || "",
    //   desiredjob: desiredjob || "",
    //   cv: cv || "",
    // });

  };

  useEffect(() => {
    fetchData();
  }, []);

  const handleSubmit = async () => {
    if ((profileForm.firstname && profileForm.lastname && profileForm.email && (userType === "Employer")) || (
      (userType === "Employee") &&
      profileForm.firstname && profileForm.lastname && profileForm.email &&
      profileForm.yearsofexp && profileForm.phonenumber && profileForm.currjobtitle &&
      profileForm.industry && profileForm.educationalbg && profileForm.desiredjob &&
      profileForm.cv)
    ) {
      const payload = { ...profileForm, userId: userId };

      const response = await fetch("https://localhost:7000/user/update",
        {
          method: "PUT",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify(payload)
        })


      if (response.ok) {
        setUsername(profileForm.firstname + " " + profileForm.lastname);
        enqueueSnackbar("Profile updated successfully", {
          variant: "success",
        });
      }
      else {
        enqueueSnackbar("Something went wrong!", { variant: "error" });
      }
    }
    else {
      enqueueSnackbar("Please fill all the fields!", { variant: "error" });
    }
  };

  return (
    <div
      style={
        displayNavbar
          ? {
            width: "calc(100% - 172px)",
            marginLeft: "166px",
            marginTop: "16px",
          }
          : { width: "99%", marginTop: "16px", marginLeft: "8px" }
      }
    >
      <Form
        title='Profile'
        fields={
          userType === "Employee"
            ? [
              { name: "First Name", type: "TextInput" },
              { name: "Last Name", type: "TextInput" },
              { name: "E-mail", type: "TextInput" },
              { name: "Phone Number", type: "TextInput" },
              { name: "Curr Job Title", type: "TextInput" },
              { name: "Industry", type: "TextInput" },
              { name: "Years of Exp", type: "TextInput" },
              { name: "Educational Bg", type: "TextInput" },
              { name: "Desired Job", type: "TextInput" },
            ]
            : [
              { name: "First Name", type: "TextInput" },
              { name: "Last Name", type: "TextInput" },
              { name: "E-mail", type: "TextInput" },
            ]
        }
        showCV={userType === "Employee" ? true : false}
        handleSubmit={handleSubmit}
        handleChange={handleChange}
        state={profileForm}
        setProfileForm={setProfileForm}
      />
    </div>
  );
};

export default ProfileContainer;
