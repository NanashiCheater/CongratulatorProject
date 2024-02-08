
import React, { Component } from 'react';
import './Home.css';

const convertBlobToBase64 = (blob) => new Promise((resolve, reject) => {
  const reader = new FileReader;
  reader.onerror = reject;
  reader.onload = () => {
      resolve(reader.result);
  };
  reader.readAsDataURL(blob);
});

const addPerson = async (id,values,constValues,inputImage,roundedImageElement) =>{
  const srcValue = roundedImageElement ? roundedImageElement.src : null;
  let resultsurname ='';
  let resultname ='';
  let resultmiddlename ='';
  let resultbirthday ='';
  let resultimage ='';
  if(values['surname']==''){
    resultsurname = constValues['surname'];
  }
  else{
    resultsurname = values['surname'];
  }
  if(values['name']==''){
    resultname = constValues['name'];
  }
  else{
    resultname = values['name'];
  }
  if(values['middlename']==''){
    resultmiddlename = constValues['middlename'];
  }
  else{
    resultmiddlename = values['middlename'];
  }
  if(values['birthday']==''){
    resultbirthday = constValues['birthday'];
  }
  else{
    resultbirthday = values['birthday'];
  }
  if (inputImage.files.length > 0) {
    const file = inputImage.files[0];
    resultimage = await convertBlobToBase64(file);
  }
  else{
    resultimage = srcValue;
  }
  console.log(resultimage);
  const newPost ={
    "id":id,
    "surname":resultsurname,
    "name":resultname,
    "middlename":resultmiddlename,
    "birthday":resultbirthday,
    "image": resultimage, 
    "imageType": resultimage.split(';')[0].split(':')[1]
  }
  console.log(JSON.stringify(newPost));
  const options ={
    method: 'PATCH',
    headers: {
        'Content-Type': 'application/json; charset=utf-8',
    },
    body: JSON.stringify(newPost)
  }
  const result = await fetch('/api/congratulator',options);
  if(result.ok){
   window.location.reload();
  }
}
const getBase = async (file) =>{
  return await convertBlobToBase64(file);
}
function handleClick(id, event) {

  const row = event.target.closest('tr');

  const inputConstElements = row.querySelectorAll('.pers_data');
  const inputElements = row.querySelectorAll('.input_data');
  const inputImage = row.querySelector('.input_image');
  const roundedImageElement = row.querySelector('.rounded-image');
  const constValues = {};

  inputConstElements.forEach(input => {
    const name = input.name;
    const value = input.value;
    constValues[name] = value;
  });

  console.log('Values:', constValues);
  const values = {};

  inputElements.forEach(input => {
    const name = input.name;
    const value = input.value;
    values[name] = value;
  });

  console.log('Values:', values);
  
  addPerson(id, values,constValues,inputImage,roundedImageElement);
}

function handleDeleteClick(personId) {
  console.log('Удалить человека с ID:', personId);
  deletePerson(personId);
  window.location.reload();
}
const deletePerson = async (id)=>{
  const response = await fetch('/api/congratulator/'+id,{method:"DELETE"});
}

export class Persons extends Component {
  static displayName = Persons.name;

  constructor(props) {
    super(props);
    this.state = { persons: [], loading: true };
  }

  componentDidMount() {
    this.renderPersonsTable();
  }

  static renderPersonsTable(persons) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Фото</th>
            <th>Фамилия</th>
            <th>Имя</th>
            <th>Отчество</th>
            <th>Дата рождения</th>
            <th>Изменить</th>
            <th>Удалить</th>
          </tr>
        </thead>
        <tbody>
          {persons.map(person =>
            <tr key={person.id}>
              <td>
                <img className='rounded-image' src={person.image} alt="img" height="128"/>
                <input className="input_image" type="file" id="photo" name="photo" accept="image/*,.png,.jpg" required/>
              </td>
              <td>
                <input className="pers_data" type="text" id="surname" name="surname"  maxLength="49" value={person.surname} required/>
                <input className="input_data" type="text" id="surname" name="surname"  maxLength="49" required/>
              </td>
              <td>
                <input className="pers_data" type="text" id="name" name="name" maxLength="49" value={person.name} required/>
                <input className="input_data" type="text" id="name" name="name" maxLength="49" required/>
              </td>
              <td>
                <input className="pers_data" type="text" id="middlename" name="middlename"maxLength="49" value={person.middlename} required/>
                <input className="input_data" type="text" id="middlename" name="middlename"maxLength="49"  required/>
              </td>
              <td>
                <input className="pers_data" type="date" id="birthday" name="birthday" maxLength="49"value={person.birthday.split("T")[0]} required/>
                <input className="input_data" type="date" id="birthday" name="birthday" maxLength="49" required/>
              </td>
              <td>
                <button className="person_button"  onClick={(event) => handleClick(person.id, event)}>Изменить</button>
              </td>
              <td>
                <button className="person_button"  onClick={() => handleDeleteClick(person.id)}>Удалить</button>
              </td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : Persons.renderPersonsTable(this.state.persons);

    return (
      <div>
        <h1 id="tabelLabel" >Все именинники</h1>
        <p>Все именинники занесённые в базу данных.</p>
        {contents}
      </div>
    );
  }

  async renderPersonsTable() {
    const response = await fetch('/api/congratulator/getall',{method:"GET"});
    const data = await response.json();
    console.log(data);
    this.setState({ persons: data, loading: false });
  }
}
