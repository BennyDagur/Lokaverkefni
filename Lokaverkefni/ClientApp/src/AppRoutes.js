import Home from "./components/Home";
import Profile from "./components/Profile";
import CreateUser from "./components/CreateUser";

const AppRoutes = [
  {
        index: true,
        element: <Home />
  },
  {
        path: 'profile/:id',
        element: <Profile />
    },
    {
        path: 'sign-up',
        element: <CreateUser />
    },
];

export default AppRoutes;
