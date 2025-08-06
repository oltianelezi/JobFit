import React, { useState, useEffect } from "react";
import JobSearch from "../../assets/images/job_search.png";
import NavbarLogo from "../../assets/images/list.svg";

import { IconButton } from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import { useContext } from "react";
import { AppContext } from "../../AppContext";


const Horizontal = () => {
  const {
    displayNavbar,
    setDisplayNavbar,
    username,
    setUsername,
    setUserType,
  } = useContext(AppContext);

  const fetchData = async () => {
    const userId = localStorage.getItem("userId");

    const response = await fetch(`https://localhost:7000/user/${userId}`);

    if (response.ok) {
      const data = await response.json();
      
      setUsername(data.firstname + " " + data.lastname);
      setUserType(data.type);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <div style={{ position: "sticky", top: "0px", zIndex: "2" }}>
      <div
        style={{
          backgroundColor: "#344966",
          margin: "0px 5px 5px 5px",
          padding: "10px",
          borderRadius: "10px",
          display: "flex",
          justifyContent: "space-between",
          height: "50px",
        }}
      >
        <div
          style={{
            display: "flex",
            gap: "10px",
            justifyContent: "center",
            width: "100px",
          }}
        >
          <img
            src={JobSearch}
            alt="test1"
            style={{
              width: "70%",
              objectFit: "contain",
              marginTop: "8px",
            }}
          />
          <IconButton
            onClick={() => {
              setDisplayNavbar(!displayNavbar);
            }}
          >
            <MenuIcon style={{ color: "white" }} />
          </IconButton>
        </div>

        <div
          style={{
            backgroundColor: "white",
            borderRadius: "10px",
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
            paddingLeft: "15px",
            paddingRight: "20px",
            minWidth: "100px",
            gap: "10px",
          }}
        >
          <div
            style={{
              width: "30px",
              height: "30px",
              borderRadius: "50%",
              backgroundColor: "black",
            }}
          ></div>
          <div>
            {
              <p>
                <strong>{username}</strong>
              </p>
            }
          </div>
        </div>
      </div>
    </div>
  );
};

export default Horizontal;
