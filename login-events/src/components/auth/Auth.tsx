import React, { FC, useState } from "react";
import { useForm } from "react-hook-form";
import * as Yup from "yup";
import { saveToken, setAuthState } from "./authSlice";
import { setUser } from "./userSlice";
import { useAppDispatch } from "./state-management/store";
import { yupResolver } from "@hookform/resolvers/yup";
import "./Auth.css";
import { User } from "../../models/User";

const schema = Yup.object().shape({
  Username: Yup.string()
    .required("What? No username?")
    .max(30, "Username cannot be longer than 16 characters"),
  Password: Yup.string().required('Without a password, "None shall pass!"'),
  Email: Yup.string().email("Please provide a valid email address (abc@xy.z)"),
});

const Auth: FC = () => {
  const {
    handleSubmit,
    register,
    formState: { errors },
  } = useForm<User>({
    resolver: yupResolver(schema),
  });
  const [isLogin, setIsLogin] = useState(true);
  const [loading, setLoading] = useState(false);
  const dispatch = useAppDispatch();

  const submitForm = (data: User) => {
    const path = isLogin ? "/auth/login" : "/auth/signup";

    fetch("http://localhost:5009/api/auth/login ", {
      method: "POST",
      headers: { "Content-type": "application/json" },
      body: JSON.stringify(data),
    })
      .then((r) => r.json())
      .then((res) => {
        console.log(res);
        data.Type = res.type;
        data.Id = res.userId;
        dispatch(saveToken("token"));
        dispatch(setUser(data));
        dispatch(setAuthState(true));
      })
      .catch((err) => {
        console.log("error");
        throw err;
      })
      .finally(() => {
        setLoading(false);
      });
  };

  return (
    <div className="auth">
      <div className="card">
        <form onSubmit={handleSubmit(submitForm)}>
          <div className="inputWrapper">
            <input
              className="loginInput"
              {...register("Username")}
              name="Username"
              placeholder="Username"
            />
            {errors && errors.Username && (
              <p className="error">{errors.Username.message}</p>
            )}
          </div>
          <div className="inputWrapper">
            <input
              className="loginInput"
              {...register("Password")}
              name="Password"
              type="password"
              placeholder="Password"
            />
            {errors && errors.Password && (
              <p className="error">{errors.Password.message}</p>
            )}
          </div>
          {!isLogin && (
            <div className="inputWrapper">
              <input
                className="loginInput"
                {...register("Email")}
                name="email"
                placeholder="Email (optional)"
              />
              {errors && errors.Email && (
                <p className="error">{errors.Email.message}</p>
              )}
            </div>
          )}
          <div className="inputWrapper">
            <button className="loginButton" type="submit" disabled={loading}>
              {isLogin ? "Login" : "Create account"}
            </button>
          </div>
          <p
            onClick={() => setIsLogin(!isLogin)}
            style={{ cursor: "pointer", opacity: 0.7 }}
          >
            {isLogin ? "No account? Create one" : "Already have an account?"}
          </p>
        </form>
      </div>
    </div>
  );
};

export default Auth;
