import React, { useEffect, useState } from "react";
import Form from "../components/shared/Form";

import JobCard from "../components/shared/JobCard";
import Modal from "../components/shared/Modal";
import { useSnackbar } from "notistack";
import { useContext } from "react";
import { AppContext } from "../AppContext";
import "./../components/shared/style/Modal.css";
import Applicants from "../components/shared/Applicants";

const SearchContainer = () => {
  const { enqueueSnackbar } = useSnackbar();
  const { userType } = useContext(AppContext);
  const [jobToUpdate, setJobToUpdate] = useState("");
  const [searchForm, setSearchForm] = useState({
    search: "",
    jobstatus: "Any",
    jobtype: "Any",
  });
  const [searchResult, setSearchResult] = useState([]);

  const [editJobForm, setEditJobForm] = useState({
    position: "",
    company: "",
    joblocation: "",
    jobstatus: "",
    jobtype: "",
  });

  const [showModal, setShowModal] = useState(false);
  const [showApplicants, setShowApplicants] = useState(false);

  const [applicants, setApplicants] = useState([]);

  const { displayNavbar } = useContext(AppContext);
  const userId = localStorage.getItem("userId");

  const handleChange = (event, name) => {
    const value = event.target.value;
    setSearchForm((prevState) => ({ ...prevState, [name]: value }));
  };

  const handleSubmit = () => {
    if (searchForm.search || searchForm.jobstatus || searchForm.jobtype) {
      fetchData();
    }
  };

  useEffect(
    () => {
      if (
        !searchForm.search &&
        searchForm.jobstatus === "Any" &&
        searchForm.jobtype === "Any" &&
        userType
      ) {
        fetchData();
      }
    },
    /*eslint-disable*/[
      searchForm.search,
      searchForm.jobstatus,
      searchForm.jobtype,
      userType,
    ]
  );


  const fetchData = async () => {
    const payload = {
      ...searchForm,
      userId: userId,
      userType: userType
    }
    const response = await fetch("https://localhost:7000/job/getJobs",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(payload)
      })

    const result = await response.json();

    const finalResult = result.map(job => ({
      position: job.position,
      company: job.company,
      joblocation: job.location,
      date: job.datePosted,
      jobtype: job.jobtype,
      jobstatus: job.jobstatus,
      jobId: job.jobId,
      userId: job.userId
    }))
    setSearchResult(finalResult);
  };

  const handleDelete = async () => {

    const response = await fetch(`https://localhost:7000/job/${jobToUpdate}`, {
      method: "DELETE"
    })

    if (response.ok) {
      enqueueSnackbar("Job deleted successfully", { variant: "success" });
    }
    else
      enqueueSnackbar("Something went wrong!", { variant: "error" });

    fetchData();
    setShowModal(false);
  };

  const handleEdit = async (jobId) => {
    setJobToUpdate(jobId);

    const response = await fetch(`https://localhost:7000/job/${jobId}`);
    const data = await response.json();

    setEditJobForm({
      position: data.position,
      company: data.company,
      joblocation: data.location,
      jobstatus: data.jobstatus,
      jobtype: data.jobtype
    });
    setShowModal(true);
  };

  const closeModal = () => {
    setShowModal(false);
  };

  const handleEditSubmit = async () => {

    const payload = { ...editJobForm, jobId: jobToUpdate };
    const response = await fetch("https://localhost:7000/job/update", {
      method: "PUT",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(payload)
    })

    if (response.ok) {
      setShowModal(false);
      enqueueSnackbar("Job updated successfully", { variant: "success" });
      fetchData();
    }
    else
      enqueueSnackbar("Something went wrong!", { variant: "error" });
  };

  const handleJobUpdate = (event, name) => {
    const value = event.target.value;
    setEditJobForm((prevState) => ({ ...prevState, [name]: value }));
  };

  const handleApply = async (jobId) => {
    const payload = {
      userId: userId,
      jobId: jobId
    }

    const response = await fetch('https://localhost:7000/application/apply', {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(payload)
    })

    if (response.ok) {
      enqueueSnackbar("Applied to job successfully successfully", { variant: "success" });
      fetchData();
    }
    else {
      enqueueSnackbar("Application failed", { variant: "error" });
    }
  };

  const handleApplicants = async (jobId) => {

    const response = await fetch(`https://localhost:7000/application/applicants/${jobId}`)
    const data = await response.json();

    setJobToUpdate(jobId);
    setShowApplicants(true);
    setApplicants(data.applicants);
  };

  const closeApplicants = () => {
    setShowApplicants(false);
  };

  return (
    <div
      style={
        displayNavbar
          ? {
            width: "calc(100% - 172px)",
            marginLeft: "166px",
            marginTop: "16px",
            backgroundColor: "#344966",
            borderRadius: "10px",
            height: "calc(100vh - 90px)",
          }
          : {
            width: "99%",
            marginTop: "16px",
            marginLeft: "8px",
            backgroundColor: "#344966",
            borderRadius: "10px",
            height: "calc(100vh - 90px)",
          }
      }
    >
      <div
        style={{
          height: "calc(100vh - 90px)",
        }}
      >
        <div>
          <Form
            showCLoseModal={false}
            title="All Jobs"
            fields={[
              { name: "Search", type: "TextInput" },
              {
                name: "Job Status",
                type: "Select",
                options: ["Any", "Open", "Closed"],
              },
              {
                name: "Job Type",
                type: "Select",
                options: ["Any", "Full - Time", "Part - Time"],
              },
            ]}
            buttonName="Apply Filter"
            handleSubmit={handleSubmit}
            handleChange={handleChange}
            state={searchForm}
          />
        </div>
        <div style={{ fontSize: "25px", color: "white", padding: "10px" }}>
          Search Result
        </div>
        <div
          style={{
            display: "flex",
            gap: "10px",
            flexFlow: "row wrap",
            justifyContent: "center",
            padding: "4px 10px 10px 10px",
            overflowY: "scroll",
            height: "calc(100vh - 330px)",
          }}
          className="jobContainer"
        >
          {searchResult.length ? (
            searchResult.map((result, index) => {
              return (
                <JobCard
                  item={result}
                  handleApplicants={handleApplicants}
                  handleEdit={handleEdit}
                  userType={userType}
                  handleApply={handleApply}
                  key={index}
                />
              );
            })
          ) : (
            <h1 style={{ color: "white" }}>
              Sorry, no results match your search
            </h1>
          )}
        </div>
      </div>
      {showModal && (
        <Modal>
          <Form
            showCloseModal={true}
            title="Edit Job"
            fields={[
              { name: "Position", type: "TextInput" },
              { name: "Company", type: "TextInput" },
              { name: "Job Location", type: "TextInput" },
              {
                name: "Job Status",
                type: "Select",
                options: ["Open", "Closed"],
              },
              {
                name: "Job Type",
                type: "Select",
                options: ["Full - Time", "Part - Time"],
              },
            ]}
            handleSubmit={handleEditSubmit}
            handleChange={handleJobUpdate}
            handleDelete={handleDelete}
            state={editJobForm}
            closeModal={closeModal}
            showDeleteButton={true}
          />
        </Modal>
      )}

      {showApplicants && (
        <Modal showBackground={true}>
          <Applicants
            closeApplicants={closeApplicants}
            applicants={applicants}
            jobId={jobToUpdate}
          />
        </Modal>
      )}
    </div>
  );
};

export default SearchContainer;
