import React, { useEffect } from 'react';
import logo from './logo.svg';
import './App.css';
import { GetAllNationalities, GetAllStudents } from './apis/students';
import { useAppDispatch, useAppSelector } from './app/hooks';
import { Header } from './componenets/Header';
import { StudentList } from './componenets/StudentList';
import { StudentForm } from './componenets/StudentForm';
import { FamilyForm } from './componenets/FamilyForm';

function App() {

  const {showStudentForm,showFamilyForm} = useAppSelector(x =>x.student)
  const dispatch = useAppDispatch()

  useEffect(() => {
    GetAllStudents(dispatch)
    GetAllNationalities(dispatch)
  }, [])


  return (
    <div className="App">
    <Header />
    <StudentList />
    {
      showStudentForm && <StudentForm />
    }
    {
      showFamilyForm && <FamilyForm />
    }
    
  </div>
  );
}

export default App;
