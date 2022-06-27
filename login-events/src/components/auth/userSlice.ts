import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { User } from "../../models/User";

const user = createSlice({
  name: "user",
  initialState: {
    user: null as User | null,
  },
  reducers: {
    setUser: (state, action) => {
      // console.log(action.payload);
      state.user = action.payload;
    },
  },
});

export const { setUser } = user.actions;
export default user.reducer;

// const user = createSlice({
//   name: "user",
//   initialState: null as User | null,
//   reducers: {
//     setUser(state, { payload }: PayloadAction<User | null>) {
//       return (state = payload != null ? payload : null);
//     },
//   },
// });
