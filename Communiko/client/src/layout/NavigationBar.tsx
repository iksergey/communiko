import React from 'react';
import { Button, Container, Menu } from 'semantic-ui-react';

export function NavigationBar() {
  return (
    <Menu fixed='top'>
      <Container>
        <Menu.Item header>
          <img src="/images/logo.png" alt="logo" />
          Communiko project
        </Menu.Item>
        <Menu.Item name='Activeness' />
        < Menu.Item >
          <Button positive content='Append Activeness' />
        </ Menu.Item >
      </Container>
    </Menu >
  );
}