export default {
  content: ['./index.html', './src/**/*.{vue.js,ts,jsx,tsx}'],
  theme: { // Define custom colors for theme here
    extend: { 
      colors: {
        "primary-color": "#00668A",
        "secondary-color": "#004E71",
      }
    },
    fontFamily: {
      Roboto: ["Roboto, sans-serif"],
    },
    container: {
      padding: "2rem",
      center: true,
    },
    screens: {
      sm: "640px",
      md: "786px"
    },
  },
  plugins: [],
}

