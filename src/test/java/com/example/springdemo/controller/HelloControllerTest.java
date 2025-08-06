package com.example.springdemo.controller;

import com.fasterxml.jackson.databind.ObjectMapper;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.http.MediaType;
import org.springframework.test.web.servlet.MockMvc;

import java.util.HashMap;
import java.util.Map;

import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.*;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;

@WebMvcTest(HelloController.class)
public class HelloControllerTest {

    @Autowired
    private MockMvc mockMvc;

    @Autowired
    private ObjectMapper objectMapper;

    @Test
    public void testHello() throws Exception {
        mockMvc.perform(get("/api/hello"))
                .andExpect(status().isOk())
                .andExpect(content().contentType(MediaType.APPLICATION_JSON))
                .andExpect(jsonPath("$.message").value("Hello, World!"))
                .andExpect(jsonPath("$.status").value("success"));
    }

    @Test
    public void testHelloWithName() throws Exception {
        String name = "Spring";
        
        mockMvc.perform(get("/api/hello/" + name))
                .andExpect(status().isOk())
                .andExpect(content().contentType(MediaType.APPLICATION_JSON))
                .andExpect(jsonPath("$.message").value("Hello, " + name + "!"))
                .andExpect(jsonPath("$.status").value("success"));
    }

    @Test
    public void testEcho() throws Exception {
        Map<String, Object> requestBody = new HashMap<>();
        requestBody.put("name", "John");
        requestBody.put("age", 30);
        requestBody.put("city", "New York");

        String jsonRequest = objectMapper.writeValueAsString(requestBody);

        mockMvc.perform(post("/api/echo")
                .contentType(MediaType.APPLICATION_JSON)
                .content(jsonRequest))
                .andExpect(status().isOk())
                .andExpect(content().contentType(MediaType.APPLICATION_JSON))
                .andExpect(jsonPath("$.echo.name").value("John"))
                .andExpect(jsonPath("$.echo.age").value(30))
                .andExpect(jsonPath("$.echo.city").value("New York"))
                .andExpect(jsonPath("$.status").value("success"))
                .andExpect(jsonPath("$.timestamp").exists());
    }

    @Test
    public void testHealth() throws Exception {
        mockMvc.perform(get("/api/health"))
                .andExpect(status().isOk())
                .andExpect(content().contentType(MediaType.APPLICATION_JSON))
                .andExpect(jsonPath("$.status").value("UP"))
                .andExpect(jsonPath("$.message").value("Application is running successfully"))
                .andExpect(jsonPath("$.timestamp").exists());
    }
}
