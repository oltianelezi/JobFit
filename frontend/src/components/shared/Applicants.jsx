import React from "react";
import CloseIcon from "@mui/icons-material/Close";
import Applicant from "./Applicant";

const Applicants = ({ applicants, closeApplicants, jobId, isEmployerInfo }) => {
  return (
    <div>
      <div
        style={{
          display: "flex",
          justifyContent: "space-between",
          width: "100%",
        }}
      >
        <div style={{ fontSize: "25px", color: "white" }}>
          {isEmployerInfo ? "Recruiter Information" : "List of Applicants"}
        </div>
        <CloseIcon
          style={{ color: "white", cursor: "pointer", marginTop: "6px" }}
          onClick={closeApplicants}
        />
      </div>

      {applicants.length === 0 ? (
        <div
          style={{
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
          }}
        >
          <h2 style={{ color: "white" }}>Sorry, no one has applied</h2>
        </div>
      ) : (
        <div
          className="applicantsList"
          style={{ maxHeight: "300px", overflowY: "auto" }}
        >
          {applicants.map((applicant) => {
            console.log(applicants);
            console.log(applicant);

            return <Applicant user={applicant} key={applicant} jobId={jobId} />;
          })}
        </div>
      )}
    </div>
  );
};

export default Applicants;
