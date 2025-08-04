// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getFirestore } from "firebase/firestore";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
const firebaseConfig = {
  apiKey: "AIzaSyCynLgDfINzGVRpo_8nnPQPqJZ-Hp0MzQA",
  authDomain: "jobapplicationvodafone.firebaseapp.com",
  projectId: "jobapplicationvodafone",
  storageBucket: "jobapplicationvodafone.appspot.com",
  messagingSenderId: "261240483812",
  appId: "1:261240483812:web:e2b6b74848adc9a083e1f1",
  measurementId: "G-ZLFKBFJK41",
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
export default getFirestore();
