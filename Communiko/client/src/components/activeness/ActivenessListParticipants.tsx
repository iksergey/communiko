import { Icon, List, Popup } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import { UserProfile } from "../../model/UserProfile";

interface Props {
  participants: UserProfile[];
}

export default observer(function ActivenessListParticipants({ participants }: Props) {
  return (
    <List horizontal>
      {
        participants?.map(item => (
          <List.Item key={item.fullName}>
            <Popup
              trigger={
                <List.Item >
                  <a onClick={() => { alert('user 1'); }}>
                    <Icon name='user' />
                    <strong>{`${item.nickName}`}</strong>
                  </a>
                </List.Item>
              }
            >
              <Popup.Content>
                <Icon name="user" />
                <strong>{item.nickName}</strong>
              </Popup.Content>
            </Popup>
          </List.Item >
        ))
      }
    </List >
  )
});