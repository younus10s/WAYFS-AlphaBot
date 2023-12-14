import { StyleSheet } from 'react-native';

const commonStyles = {
  borderRadius: 1,
  borderColor: 'black',
  borderWidth: 1,
};

const commonJoystickStyle = {
  alignItems: 'center',
  borderRadius: 100,
  borderColor: 'black',
  borderWidth: 1,
  elevation: 5,
  width: 70,
  height: 70,
  justifyContent: 'center',
};

const styles = StyleSheet.create({
  container: {
    alignItems: 'center',
    backgroundColor: '#fff',
    flex: 1,
    justifyContent: 'center',
  },

  arrow: {
    height: 30,
    width: 30,
  },

  button: commonStyles,

  item: {
    marginHorizontal: 30,
  },

  joystickContainer: {
    alignItems: 'center',
    bottom: 20,
    height: 200,
    justifyContent: 'center',
    left: 20,
    position: 'absolute',
    width: 200,
  },

  joystickCell: {
    alignItems: 'center',
    justifyContent: 'center',
  },  

  joystickCircle: {
    ...commonJoystickStyle,
    width: 130,
    height: 130,
  },

  joystickCircle2: {
    ...commonJoystickStyle,
    backgroundColor: 'black',
    shadowColor: '#000',
    shadowOffset: {
      width: 1,
      height: 2,
    },
    shadowOpacity: 0.5,
    shadowRadius: 3.84,
  },

  joystickCircle2Pressed: {
    ...commonJoystickStyle,
    backgroundColor: 'gray',
  },

  joystickRow: {
    alignItems: 'center',
    flexDirection: 'row',
    justifyContent: 'space-around',
  },

  mapContainer: {
    alignItems: 'center',
    height: 260,
    justifyContent: 'center',
    left: 1,
    position: 'absolute',
    bottom: 1,
    width: 447,
  },

  mapCell: {
    alignItems: 'center',
    height: '100%',
    justifyContent: 'center',
    width: '20%',
  },

  mapCellImg: {
    height: '100%',
    width: '100%',
  },

  mapRow: {
    alignItems: 'center',
    flexDirection: 'row',
    height: '20%',
    justifyContent: 'space-around',
    width: '100%',
  },

  pac: {
    height: 50,
    position: 'absolute',
    width: 50,
  },

  shutdownButton: commonStyles,

  showContainer: {
    alignItems: 'center',
    bottom: 20,
    height: 300,
    justifyContent: 'center',
    position: 'absolute',
    right: 20,
    width: 450,
  },

  statusContainer: {
    alignItems: 'left',
    height: 50,
    justifyContent: 'center',
    left: 20,
    position: 'absolute',
    top: 40,
    width: 200,
  },

  toggleContainer: {
    alignItems: 'center',
    flexDirection: 'row',
    height: 20,
    justifyContent: 'space-around',
    left: 1,
    position: 'absolute',
    top: 1,
    width: 150,
  },
});

export default styles;
