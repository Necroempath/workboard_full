

function App() {
  return (
    <>
     <div className="min-h-screen bg-gray-900 flex items-center justify-center">
      <div className="bg-white rounded-2xl shadow-xl p-8 max-w-sm w-full">
        <h1 className="text-2xl font-bold text-gray-800 mb-4">
          Tailwind Test
        </h1>

        <p className="text-gray-600 mb-6">
          If styles work, everything is set up correctly.
        </p>

        <button className="w-full bg-blue-500 hover:bg-blue-600 text-white font-semibold py-2 px-4 rounded-lg transition">
          Click me
        </button>
      </div>
    </div>
    </>
  )
}

export default App
