'use client'

import React, { useRef } from "react";
import { AppStore, makeStore } from "./store";
import { Provider } from "react-redux";


interface ProviderProps{
    children?: React.ReactNode;
}

// export default function StoreProvider({children}:ProviderProps):React.FC<ProviderProps> {
//     const storeRef =useRef<AppStore>();
export default function StoreProvider({ children }: ProviderProps): JSX.Element {
        const storeRef = useRef<AppStore>();
      

    if(!storeRef.current){
        storeRef.current=makeStore()
    }


    return (
        <Provider store={storeRef.current}>
          {children}
        </Provider>
      );

    // return <Provider store={storeRef.current} children={children}>{children}</Provider>
}