import React, { useState } from "react";
import "./style/SignInContainer.css";
import JobSearchImage from "../assets/images/job_search.png";
import TextInput from "../components/shared/TextInput";
import Button from "../components/shared/Button";
import { useSnackbar } from "notistack";
import db from "../firebase";
import { query, where, collection, getDocs } from "firebase/firestore";
import { useNavigate } from "react-router-dom";

const SignInContainer = () => {
  const [formData, setFormData] = useState({ username: "", password: "" });
  const [isLoading, setIsLoading] = useState(false);
  const { enqueueSnackbar } = useSnackbar();
  const navigate = useNavigate();

  const handleChange = (event, name) => {
    const value = event.target.value;
    setFormData((prevState) => ({ ...prevState, [name]: value }));
  };

  const handleSubmit = async (e) => {
    setIsLoading(true);
    if (formData.username && formData.password) {
      // const response = query(
      //   collection(db, "users"),
      //   where("username", "==", formData.username),
      //   where("password", "==", formData.password)
      // );

      const response = await fetch("https://localhost:7000/user/login",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json"
          },
          body: JSON.stringify(formData)
        }
      )
      const data = await response.json();
      if (response.ok) {
        localStorage.setItem("userId", data.userId);
        setIsLoading(false);
        navigate("/jobs");
        console.log(data.userId);
        
      }
      else {
        enqueueSnackbar("User not found!", { variant: "error" });
        setIsLoading(false);
      }

    } else {
      enqueueSnackbar("Please fill in the fields!", { variant: "error" });
      setIsLoading(false);
    }
  };

  return (
    <div>
      <div className='signIn'>
        <div className='signIn_image'>
          <img
            src={JobSearchImage}
            alt='Job search'
            className='signIn_image_job'
          />
        </div>
        <div className='signIn_form'>
          <div className='signIn_head'>Sign in to JobFit</div>
          <div className='signIn_para'>
            New user?
            <a className='createAccount' href='/signup'>
              Create account.
            </a>
          </div>
          <form className='signIn_form_input'>
            <div className='form_input'>
              <TextInput
                type='text'
                name='username'
                placeholder='Username *'
                onChange={(event) => handleChange(event, "username")}
                style={{
                  border: "none",
                  borderRadius: "10px",
                  width: "250px",
                  height: "40px",
                  paddingLeft: "10px",
                }}
              />
              <TextInput
                type='password'
                name='password'
                placeholder='Password *'
                onChange={(event) => handleChange(event, "password")}
                style={{
                  border: "none",
                  borderRadius: "10px",
                  width: "250px",
                  height: "40px",
                  marginTop: "10px",
                  marginBottom: "20px",
                  paddingLeft: "10px",
                }}
              />
              <Button
                name='Log in'
                isLoading={isLoading}
                handleClick={handleSubmit}
                style={{
                  width: "260px",
                  height: "38px",
                  backgroundColor: "white",
                  color: "black",
                  cursor: "pointer",
                  border: "none",
                  borderRadius: "10px",
                }}
              />
            </div>
          </form>
        </div>
      </div>
    </div>
  );
};

export default SignInContainer;
