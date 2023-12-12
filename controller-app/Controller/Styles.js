import { StyleSheet } from 'react-native';

const styles = StyleSheet.create({
    container: {
      flex: 1,
      backgroundColor: '#fff',
      alignItems: 'center',
      justifyContent: 'center',
    },

    joystickContainer: {
      //backgroundColor: "yellow",
      position: 'absolute',
      width: 200,
      height: 200,
      left: 20,
      bottom: 20,
      alignItems: 'center',
      justifyContent: 'center',
    },

    joystickRow: {
      flexDirection: 'row',
      justifyContent: 'space-around',
      alignItems: 'center',
      justifyContent: 'center',
    },

    joystickCell: {
        alignItems: 'center',
        justifyContent: 'center',

    },

    arrow: {
        width: 30,
        height: 30
    },

    joystickCircle: {
        width: 130,
        height: 130,
        alignItems: 'center',
        justifyContent: 'center',
        borderRadius: 100,
        borderColor: "black",
        borderWidth: 1
    },

    joystickCircle2: {
      backgroundColor: "black",
      width: 70,
      height: 70,
      alignItems: 'center',
      justifyContent: 'center',
      borderRadius: 100,
      borderColor: "black",
      borderWidth: 1,
      shadowColor: "#000",
      shadowOffset: {
      width: 1,
      height: 2,
      },
      shadowOpacity: 0.5,
      shadowRadius: 3.84,
      elevation: 5,
    },

    joystickCircle2Pressed: {
      backgroundColor: "gray",
      width: 70,
      height: 70,
      alignItems: 'center',
      justifyContent: 'center',
      borderRadius: 100,
      borderColor: "black",
      borderWidth: 1,
      shadowColor: "#000",
      shadowOffset: {
      width: 1,
      height: 2,
      },
      shadowOpacity: 0.5,
      shadowRadius: 3.84,
      elevation: 5,
    },

    statusContainer: {
      //backgroundColor: "gray",
      position: 'absolute',
      width: 200,
      height: 50,
      top:40,
      left: 20,
      alignItems: 'left',
      justifyContent: 'center',
    },

    showContainer: {
      //backgroundColor: "gray",
      position: 'absolute',
      width: 450,
      height: 300,
      right: 20,
      bottom: 20,
      alignItems: 'center',
      justifyContent: 'center',
    },

    toggleContainer: {
      //backgroundColor: "white",
      position: 'absolute',
      width: 150,
      height: 20,
      left: 1,
      top: 1,
      alignItems: 'center',
      justifyContent: 'center',
      flexDirection: 'row',
      alignItems: 'center',
      justifyContent: 'space-around',
    },

    item: {
      marginHorizontal: 30,
    },

    mapContainer: {
      //backgroundColor: "gray",
      position: 'absolute',
      width: 447,
      height: 260,
      left: 1,
      bottom: 1,
      alignItems: 'center',
      justifyContent: 'center',
    },

    mapRow: {
      //backgroundColor: "blue",
      flexDirection: 'row',
      justifyContent: 'space-around',
      alignItems: 'center',
      justifyContent: 'center',
      width: "100%",
      height: "20%",
    },

    mapCell: {
        alignItems: 'center',
        justifyContent: 'center',
        width: "20%",
        height: "100%",

    },
    mapCellImg: {
      width: "100%",
      height: "100%"
    },

    pac: {
      position: 'absolute',
      width: 50,
      height: 50,
    },

    button: {
      borderRadius: 1,
      borderColor: 'black',
      borderWidth: 1,
    },

  });

export default styles;