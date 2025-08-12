import React, { useEffect, useState } from "react";
import Form from "../components/shared/Form";

import JobCard from "../components/shared/JobCard";
import Modal from "../components/shared/Modal";
import { useSnackbar } from "notistack";
import { useContext } from "react";
import { AppContext } from "../AppContext";
import "./../components/shared/style/Modal.css";
import Applicants from "../components/shared/Applicants";

const AppliedJobsContainer = () => {
  const { enqueueSnackbar } = useSnackbar();
  const { userType } = useContext(AppContext);
  const [jobToUpdate, setJobToUpdate] = useState("");

  const [searchResult, setSearchResult] = useState([]);


  const [showApplicants, setShowApplicants] = useState(false);

  const [applicants, setApplicants] = useState([]);

  const { displayNavbar } = useContext(AppContext);
  const userId = localStorage.getItem("userId");

  const fetchData = async () => {
    const response = await fetch(`https://localhost:7000/job/getJobsAppliedTo/${userId}`)

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

  useEffect(() => {
    fetchData();
  }, []);


  const handleEmployerInfo = async (jobId) => {

    const response = await fetch(`https://localhost:7000/user/getEmployerInfo/${jobId}`)
    const data = await response.json();
    
    setJobToUpdate(jobId);
    setShowApplicants(true);
    setApplicants([data]);
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
        <div style={{ fontSize: "25px", color: "white", padding: "10px" }}>
          Applied Jobs
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
                  handleApplicants={handleEmployerInfo}
                  userType={userType}
                  key={index}
                  isAppliedJobs={true}
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
      {showApplicants && (
        <Modal showBackground={true}>
          <Applicants
            closeApplicants={closeApplicants}
            applicants={applicants}
            jobId={jobToUpdate}
            isEmployerInfo={true}
          />
        </Modal>
      )}
    </div>
  );
};

export default AppliedJobsContainer;
