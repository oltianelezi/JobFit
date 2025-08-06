import React, { useState } from "react";
import Form from "../components/shared/Form";

import { useSnackbar } from "notistack";
import { useContext } from "react";
import { AppContext } from "../AppContext";
const AddJobContainer = () => {
  const userId = parseInt(localStorage.getItem("userId"), 10);
  const { enqueueSnackbar } = useSnackbar();

  const { displayNavbar } = useContext(AppContext);

  const [addJobForm, setAddJobForm] = useState({
    position: "",
    company: "",
    joblocation: "",
    jobstatus: "Open",
    jobtype: "Full - Time",
    userId: userId,
  });

  const handleChange = (event, name) => {
    const value = event.target.value;
    setAddJobForm((prevState) => ({ ...prevState, [name]: value }));
  };

  const addJob = async () => {
    if (
      addJobForm.position &&
      addJobForm.jobtype &&
      addJobForm.company &&
      addJobForm.jobstatus &&
      addJobForm.joblocation
    ) {
      const date = new Date().toISOString().split('T')[0];  // This returns "2025-08-06"
      const payload = { ...addJobForm, date };
      const response = await fetch("https://localhost:7000/job/addJob",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify(payload)
        }
      )
      if (response.ok) {
        enqueueSnackbar("Job added successfully", { variant: "success" });
      }
      else {
        enqueueSnackbar("Something went wrong!", { variant: "error" });
      }
      setAddJobForm({
        position: "",
        company: "",
        joblocation: "",
        jobstatus: "Open",
        jobtype: "Full - Time",
        userId: userId,
        date: "",
        apply: [],
      });
    } else {
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
        title='Add Job'
        fields={[
          { name: "Position", type: "TextInput" },
          { name: "Company", type: "TextInput" },
          { name: "Job Location", type: "TextInput" },
          { name: "Job Status", options: ["Open", "Closed"] },
          {
            name: "Job Type",
            options: ["Full - Time", "Part - Time"],
          },
        ]}
        handleSubmit={addJob}
        handleChange={handleChange}
        state={addJobForm}
      />
    </div>
  );
};

export default AddJobContainer;
