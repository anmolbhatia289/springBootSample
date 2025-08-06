package com.example.springdemo.controller;

import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.web.client.TestRestTemplate;
import org.springframework.boot.test.web.server.LocalServerPort;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;

@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
public class ErrorHandlingTest {

    @LocalServerPort
    private int port;

    @Autowired
    private TestRestTemplate restTemplate;

    @Test
    public void testNotFoundError() throws Exception {
        String url = "http://localhost:" + port + "/api/nonexistent";
        ResponseEntity<String> response = restTemplate.getForEntity(url, String.class);
        
        assert response.getStatusCode() == HttpStatus.NOT_FOUND;
        assert response.getBody().contains("404");
        assert response.getBody().contains("error");
    }

    @Test
    public void testRootPathNotFound() throws Exception {
        String url = "http://localhost:" + port + "/";
        ResponseEntity<String> response = restTemplate.getForEntity(url, String.class);
        
        assert response.getStatusCode() == HttpStatus.NOT_FOUND;
        assert response.getBody().contains("404");
    }

    @Test
    public void testRandomInvalidPath() throws Exception {
        String url = "http://localhost:" + port + "/totally/random/invalid/path";
        ResponseEntity<String> response = restTemplate.getForEntity(url, String.class);
        
        assert response.getStatusCode() == HttpStatus.NOT_FOUND;
        assert response.getBody().contains("404");
        String body = response.getBody();
        assert body.contains("Not Found") || body.contains("error");
    }

    @Test
    public void testHealthEndpointWorks() throws Exception {
        String url = "http://localhost:" + port + "/api/health";
        ResponseEntity<String> response = restTemplate.getForEntity(url, String.class);
        
        assert response.getStatusCode() == HttpStatus.OK;
        assert response.getBody().contains("UP");
        assert response.getBody().contains("Application is running successfully");
    }
}
