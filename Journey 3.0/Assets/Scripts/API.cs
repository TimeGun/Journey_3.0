using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class API
{
    //returns the single instance of the required manager
    private static T FindSingleInstance<T> () where T : Object {
        if (Application.isEditor) {
            T[] result = GameObject.FindObjectsOfType( typeof (T)) as T[];
            if (result.Length == 0 ) {
                throw new System.Exception( "API: can't find module " + typeof (T) +
                                            " in the scene!" );
            }
            if (result.Length > 1 ) {
                throw new System.Exception( "API: there is more than one " +
                                            typeof (T) + " in the scene!" );
            }
            if (result[ 0 ] is T) {
                return result[ 0 ];
            } else {
                throw new System.Exception( "API: there is a type mismatch with " +
                                            typeof (T) + "!" );
            }
        } else {
            return GameObject.FindObjectOfType( typeof (T)) as T;
        }
    }
    
    private static GlobalReferences _globalReferencesInstance;

    private static InterestManagerScript _interestManagerScriptInstance;


    public static GlobalReferences GlobalReferences {
        get {
            if (_globalReferencesInstance == null ||
                ReferenceEquals(_globalReferencesInstance, null)) {
                _globalReferencesInstance = FindSingleInstance<GlobalReferences>();
            }
            return _globalReferencesInstance;
        }
    }
    
    public static InterestManagerScript InterestManagerScript {
        get {
            if (_interestManagerScriptInstance == null ||
                ReferenceEquals(_interestManagerScriptInstance, null)) {
                _interestManagerScriptInstance = FindSingleInstance<InterestManagerScript>();
            }
            return _interestManagerScriptInstance;
        }
    }
    
    
    
    public static bool PrewarmReferences () {
        if (GlobalReferences) {
            return true ;
        }
        return false ;
    }
}
