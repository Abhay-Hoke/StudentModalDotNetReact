'use client'
import React from 'react';
import { useRef } from 'react'
import { Provider } from 'react-redux'
import { makeStore, AppStore } from './store.ts'

interface ProviderProps {
    children?: React.ReactNode;
  }
export default function StoreProvider({ children }: ProviderProps): React.FC<ProviderProps> {
  const storeRef = useRef<AppStore>()
  if (!storeRef.current) {
    // Create the store instance the first time this renders
    storeRef.current = makeStore()
  }

  return <Provider store={storeRef.current} children={children}>{children}</Provider>
}
