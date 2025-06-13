// Simulated user and posts data
const users = [
  { id: 1, name: "Alice", email: "alice@example.com" },
  { id: 2, name: "Bob", email: "bob@example.com" }
];

const posts = [
  { id: 101, userId: 1, title: "Alice's First Post" },
  { id: 102, userId: 2, title: "Bob's Post" },
  { id: 103, userId: 1, title: "Alice's Second Post" }
];

// 1. Basic Fetch-like simulation (Promise-based)
function fakeFetchUser(id) {
  return new Promise((resolve, reject) => {
    setTimeout(() => {
      const user = users.find(u => u.id === id);
      user ? resolve(user) : reject("User not found");
    }, 500);
  });
}

// 2. Promise Chaining
function fakeFetchPostsByUser(userId) {
  return new Promise((resolve) => {
    setTimeout(() => {
      const userPosts = posts.filter(p => p.userId === userId);
      resolve(userPosts);
    }, 500);
  });
}

fakeFetchUser(1)
  .then(user => {
    console.log("Promise Chain - User:", user);
    return fakeFetchPostsByUser(user.id);
  })
  .then(userPosts => {
    console.log("Promise Chain - Posts:", userPosts);
  })
  .catch(err => console.error("Promise Chain - Error:", err));

// 3. Async/Await
async function asyncExample() {
  try {
    const user = await fakeFetchUser(2);
    console.log("\nAsync/Await - User:", user);

    const userPosts = await fakeFetchPostsByUser(user.id);
    console.log("Async/Await - Posts:", userPosts);
  } catch (error) {
    console.error("Async/Await - Error:", error);
  }
}
asyncExample();

// 4. Callback Hell Simulation
function getUserCallback(id, callback) {
  setTimeout(() => {
    const user = users.find(u => u.id === id);
    if (user) {
      console.log("\nCallback Hell - Got user");
      callback(null, user);
    } else {
      callback("User not found");
    }
  }, 500);
}

function getPostsCallback(userId, callback) {
  setTimeout(() => {
    const userPosts = posts.filter(p => p.userId === userId);
    console.log("Callback Hell - Got posts");
    callback(null, userPosts);
  }, 500);
}

getUserCallback(1, (err, user) => {
  if (err) return console.error("Callback Hell - Error:", err);
  getPostsCallback(user.id, (err, userPosts) => {
    if (err) return console.error("Callback Hell - Error:", err);
    console.log("Callback Hell - User:", user);
    console.log("Callback Hell - Posts:", userPosts);
  });
});
