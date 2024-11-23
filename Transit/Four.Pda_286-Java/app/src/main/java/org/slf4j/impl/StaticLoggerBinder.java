package org.slf4j.impl;

import org.slf4j.ILoggerFactory;
import org.slf4j.spi.LoggerFactoryBinder;

import four.pda.logs.LoggerFactory;

/**
 * Created by asavinova on 21/10/15.
 */
public class StaticLoggerBinder implements LoggerFactoryBinder {

	public static final StaticLoggerBinder SINGLETON = new StaticLoggerBinder();

	public static StaticLoggerBinder getSingleton() {
		return SINGLETON;
	}

	@Override
	public ILoggerFactory getLoggerFactory() {
		return new LoggerFactory();
	}

	@Override
	public String getLoggerFactoryClassStr() {
		return LoggerFactory.class.getName();
	}

}
