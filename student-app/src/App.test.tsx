import React from 'react';
import { render, screen } from '@testing-library/react';
import { Provider } from 'react-redux';
import App from './App';
import  { makeStore }  from './app/store';

test('renders learn react link', () => {
  const store = makeStore();
  const{getByText}= render(
  <Provider store={ store }>
  <App />
  </Provider>
  
  );
 
  expect(getByText(/learn/i)).toBeInTheDocument();
});
