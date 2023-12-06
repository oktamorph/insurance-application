USE [master]
GO

/****** Object:  Database [InsuranceDb]    Script Date: 12/6/2023 1:39:24 AM ******/
CREATE DATABASE [InsuranceDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'InsuranceDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\InsuranceDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'InsuranceDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\InsuranceDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
