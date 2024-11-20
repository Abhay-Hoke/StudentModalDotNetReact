import React, { useEffect } from 'react';
import './App.css';
import { useDispatch } from 'react-redux';
import { Header } from './components/Header.tsx';
import { StudentList } from './components/StudentList.tsx';
import { StudentForm } from './components/StudentForm.tsx';
import { useAppSelector } from './app/hooks.ts';
import { GetAllNationalities, GetAllStudents } from './apis/students.ts';
import { FamilyForm } from './components/FamilyForm.tsx';

function App() {
  const {showStudentForm,showFamilyForm} = useAppSelector(x =>x.student)
  const dispatch = useDispatch()

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
