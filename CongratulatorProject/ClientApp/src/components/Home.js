
import React, { Component } from 'react';
import './Home.css';


export class Home extends Component {
  static displayName = Home.name;

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
          </tr>
        </thead>
        <tbody>
          {persons.map(person =>
            <tr key={person.id}>
              <td><img className='rounded-image' src={person.image} alt="img" height="128"/></td>
              <td>{person.surname}</td>
              <td>{person.name}</td>
              <td>{person.middlename}</td>
              <td>{person.birthday.split("T")[0]}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : Home.renderPersonsTable(this.state.persons);

    return (
      <div>
        <h1 id="tabelLabel" >Ближайшие именинники</h1>
        <p>Здесь показаны люди у которых день рождения в ближайщие 7 дней.</p>
        {contents}
      </div>
    );
  }

  async renderPersonsTable() {
    const response = await fetch('/api/congratulator/getmany/7',{method:"GET"});
    const data = await response.json();
    console.log(data);
    this.setState({ persons: data, loading: false });
  }
}
