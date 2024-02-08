import { Home } from "./components/Home";
import AddPerson from "./components/AddPerson";
import { Persons } from "./components/Persons";

const AppRoutes = [
  {
    
    index: true,
    element: <Home />
  },
  {    
    path: '/add',
    element: <AddPerson />
  },
  {
    path: '/persons',
    element: <Persons />
  }  
];

export default AppRoutes;
