import {  useState, useEffect } from "react";
import './AddPerson.css';

export const AddPerson = () =>{
    const [file, setFile] = useState();
    const [image, setImage] = useState();
    function handleChange(e) {
        console.log(e.target.files);
        setFile(e.target.files[0]);
        setImage(URL.createObjectURL(e.target.files[0]));
    }
   
    function handleClick(e) {
        e.preventDefault()
        addPerson();
    }

    
    const addPerson = async () =>{
        // let fileBase64 = '';
        // getBase64(file, (result) => {
        //     fileBase64 = result;
        // });
        
        
        const surname=document.getElementById('surname').value;
        const name=document.getElementById('name').value;
        const middlename=document.getElementById('middlename').value;
        const birthday=document.getElementById('birthday').value;
        const image=document.getElementById('photo').value;
        if(surname!=''&&name!=''&&middlename!=''&&birthday!=''&&image!=''){
            const base64String = await convertBlobToBase64(file);
            const newPost ={
                "id":0,
                "surname":surname,
                "name":name,
                "middlename":middlename,
                "birthday":birthday,
                "image": base64String, 
                "imageType": file.type       
            }
            console.log(JSON.stringify(newPost));
            const options ={
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json; charset=utf-8',
                    },
                    body: JSON.stringify(newPost)
            }
            const result = await fetch('/api/congratulator',options);
            if(result.ok){
                window.alert("Успешно добавлен!");
                window.location.reload();
            }
        }
        else{
            window.alert("Не все поля заполнены!");
        }
    }
    const convertBlobToBase64 = (blob) => new Promise((resolve, reject) => {
        const reader = new FileReader;
        reader.onerror = reject;
        reader.onload = () => {
            resolve(reader.result);
        };
        reader.readAsDataURL(blob);
    });
    return (
            <div className="person_body" >
                <form className="person_form" >
                    <label className="form_label">Фамилия:</label>
                    <input className="input_data" type="text" id="surname" name="surname"  maxLength="49" required/>

                    <label className="form_label" >Имя:</label>
                    <input className="input_data" type="text" id="name" name="name" maxLength="49" required/>

                    <label className="form_label" >Отчество:</label>
                    <input className="input_data" type="text" id="middlename" name="middlename"maxLength="49"  required/>

                    <label className="form_label" >День рождения:</label>
                    <input className="input_data" type="date" id="birthday" name="birthday" maxLength="49" required/>

                    <label className="form_label"  >Фотография:</label>
                    <input className="input_data" type="file" id="photo" name="photo" accept="image/*,.png,.jpg" onChange={handleChange} required/>

                    <button className="person_button"  onClick={handleClick}>Отправить</button>
                </form>
            </div>
    );
}

export default AddPerson;